using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Domain.Model.Enum;
using Renova.Persistence;
using Renova.Service.Commands.Movimentacao;

namespace Renova.Service.Handlers.Movimentacao
{
    public class CriarMovimentacaoCommandHandler : IRequestHandler<CriarMovimentacaoCommand, MovimentacaoModel>
    {
        private readonly RenovaDbContext _context;
        private readonly IMediator _mediator;
        public CriarMovimentacaoCommandHandler(RenovaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<MovimentacaoModel> Handle(CriarMovimentacaoCommand request, CancellationToken cancellationToken)
        {
            if (request.Produtos == null || !request.Produtos.Any())
                throw new ValidationException("Nenhum produto informado para a movimentação.");

            var produtosMovimentacao = new List<ProdutoMovimentacaoModel>();
            decimal valorTotal = 0;

            foreach (var produtoDto in request.Produtos)
            {
                var produto = await _context.Produto
                    .FirstOrDefaultAsync(p => p.Referencia == produtoDto.ProdutoReferencia && p.LojaId == request.LojaId, cancellationToken);

                if (produto == null)
                    throw new KeyNotFoundException($"Produto não encontrado: {produtoDto.ProdutoReferencia}");

                if (produto.Status != StatusProdutoEnum.Disponivel)
                    throw new ValidationException($"Produto não disponível: {produtoDto.ProdutoReferencia}");

                produtosMovimentacao.Add(new ProdutoMovimentacaoModel
                {
                    LojaId = request.LojaId,
                    ProdutoId = produto.Id,
                    Valor = produtoDto.Valor ?? produto.Preco
                });

                valorTotal += produtoDto.Valor ?? produto.Preco ?? 0;

                // Atualiza o status do produto
                produto.Status = request.Tipo switch
                {
                    TipoMovimentacaoEnum.Venda => StatusProdutoEnum.Vendido,
                    TipoMovimentacaoEnum.DevolucaoVenda => StatusProdutoEnum.Disponivel,
                    TipoMovimentacaoEnum.Devolucao => StatusProdutoEnum.Devolvido,
                    TipoMovimentacaoEnum.Doacao => StatusProdutoEnum.Doado,
                    TipoMovimentacaoEnum.Emprestimo => StatusProdutoEnum.Emprestado,
                    TipoMovimentacaoEnum.DevolucaoEmprestimo => StatusProdutoEnum.Disponivel,
                    _ => StatusProdutoEnum.Vendido
                };

                // Marca a entidade como modificada (não obrigatório se a entidade foi carregada pelo DbContext)
                _context.Produto.Update(produto);
            }
            var movimentacao = new MovimentacaoModel
            {
                Id = Guid.NewGuid(),
                LojaId = request.LojaId,
                Tipo = request.Tipo ?? TipoMovimentacaoEnum.Venda,
                Data = request.Data ?? DateTime.UtcNow,
                ProdutoMovimentacoes = produtosMovimentacao
            };
            foreach (var pm in produtosMovimentacao)
            {
                pm.MovimentacaoId = movimentacao.Id;
            }

            var contasAReceber = new ContasAReceberModel()
            {
                Valor = valorTotal,
                Status = StatusContaEnum.Pendente,
                NumParcela = 1,
                TotParcela = 1,
                MovimentacaoId = movimentacao.Id,
                LojaId = request.LojaId,
                DataVencimento = DateTime.UtcNow.AddDays(30)
            };

            await _context.ContasAReceber.AddAsync(contasAReceber, cancellationToken);

            var contasAPagar = new ContasAPagarModel()
            {
                Valor = valorTotal,
                Status = StatusContaEnum.Pendente,
                NumParcela = 1,
                TotParcela = 1,
                MovimentacaoId = movimentacao.Id,
                LojaId = request.LojaId,
                DataVencimento = DateTime.UtcNow.AddDays(30)
            };

            await _context.ContasAPagar.AddAsync(contasAPagar, cancellationToken);

            await _context.Movimentacao.AddAsync(movimentacao, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return movimentacao;
        }
    }
}
