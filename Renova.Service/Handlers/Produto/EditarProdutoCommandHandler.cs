using MediatR;
using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Commands.Produto;
using Renova.Service.Queries.Produto;

namespace Renova.Service.Handlers.Produto
{
    public class EditarProdutoCommandHandler : IRequestHandler<EditarProdutoCommand, ProdutoModel>
    {
        private readonly RenovaDbContext _context;
        private readonly IMediator _mediator;
        public EditarProdutoCommandHandler(RenovaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ProdutoModel> Handle(EditarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await _mediator.Send(new GetProdutoByIdQuery() { Id = request.Id, LojaId = request.LojaId });

            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado");
            }

            if (request.FornecedorId != null)
            {
                produto.FornecedorId = request.FornecedorId;
            }
            if (request.Preco != null)
            {
                produto.Preco = request.Preco;
            }
            if (request.Status != null)
            {
                produto.Status = request.Status ?? produto.Status;
            }
            if (request.CorId != null)
            {
                produto.CorId = request.CorId;
            }
            if (request.TamanhoId != null)
            {
                produto.TamanhoId = request.TamanhoId;
            }
            if (request.MarcaId != null)
            {
                produto.MarcaId = request.MarcaId;
            }
            if (request.TipoId != null)
            {
                produto.TipoId = request.TipoId;
            }
            if (!string.IsNullOrWhiteSpace(request.Descricao))
            {
                produto.Descricao = request.Descricao;
            }
            if (request.DataEntrada != null)
            {
                produto.DataEntrada = request.DataEntrada ?? produto.DataEntrada;
            }

            _context.Produto.Update(produto);
            await _context.SaveChangesAsync();

            return produto;

        }
    }
}
