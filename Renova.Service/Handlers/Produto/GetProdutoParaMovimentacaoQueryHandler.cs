using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Domain.Model.Enum;
using Renova.Persistence;
using Renova.Service.Queries.Produto;

namespace Renova.Service.Handlers.Produto
{
    public class GetProdutoParaMovimentacaoQueryHandler : IRequestHandler<GetProdutoParaMovimentacaoQuery, ProdutoModel?>
    {
        private readonly RenovaDbContext _context;

        public GetProdutoParaMovimentacaoQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<ProdutoModel?> Handle(GetProdutoParaMovimentacaoQuery request, CancellationToken cancellationToken)
        {
            var produto = await _context.Produto.
                FirstOrDefaultAsync(produto => (produto.Id == request.Id) &&
                produto.LojaId == request.LojaId,
                cancellationToken);

            var permiteMovimentacao = request.TipoMovimentacao switch
            {
                TipoMovimentacaoEnum.Venda => produto.Status == StatusProdutoEnum.Disponivel,
                TipoMovimentacaoEnum.DevolucaoVenda => produto.Status == StatusProdutoEnum.Vendido,
                TipoMovimentacaoEnum.Devolucao => produto.Status == StatusProdutoEnum.Disponivel,
                TipoMovimentacaoEnum.Doacao => produto.Status == StatusProdutoEnum.Disponivel,
                TipoMovimentacaoEnum.Emprestimo => produto.Status == StatusProdutoEnum.Disponivel,
                TipoMovimentacaoEnum.DevolucaoEmprestimo => produto.Status == StatusProdutoEnum.Emprestado,
                _ => false
            };

            if (!permiteMovimentacao)
            {
                throw new ValidationException($"Produto com Status {produto.Status} não pode ser utilizado em movimentação com Tipo {request.TipoMovimentacao}");
            }

            return produto;
        }
    }
}
