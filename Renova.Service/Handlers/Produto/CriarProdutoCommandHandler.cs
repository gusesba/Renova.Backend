using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Domain.Model.Enum;
using Renova.Persistence;
using Renova.Service.Commands.Produto;


namespace Renova.Service.Handlers.Produto
{
    public class CriarProdutoCommandHandler : IRequestHandler<CriarProdutoCommand, ProdutoModel>
    {
        private readonly RenovaDbContext _context;
        private readonly IMediator _mediator;
        public CriarProdutoCommandHandler(RenovaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ProdutoModel> Handle(CriarProdutoCommand request, CancellationToken cancellationToken)
        {
            var proximaReferencia = await _context.Produto
                .Where(p => p.LojaId == request.LojaId)
                .MaxAsync(p => (int?)p.Referencia,cancellationToken) ?? 0;

            var produto = new ProdutoModel()
            {
                LojaId = request.LojaId,
                FornecedorId = request.FornecedorId,
                Preco = request.Preco,
                Status = request.Status ?? StatusProdutoEnum.Disponivel,
                CorId = request.CorId,
                TamanhoId = request.TamanhoId,
                MarcaId = request.MarcaId,
                TipoId = request.TipoId,
                Descricao = request.Descricao,
                DataEntrada = request.DataEntrada ?? DateTime.UtcNow,
                Referencia = proximaReferencia + 1
            };

            var createdProduto = await _context.Produto.AddAsync(produto, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return createdProduto.Entity;
        }
    }
}
