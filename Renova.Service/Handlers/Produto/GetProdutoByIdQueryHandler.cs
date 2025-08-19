using Renova.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Service.Queries.Produto;

namespace Renova.Service.Handlers.Produto
{
    public class GetProdutoByIdQueryHandler : IRequestHandler<GetProdutoByIdQuery,ProdutoModel?>
    {
        private readonly RenovaDbContext _context;

        public GetProdutoByIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<ProdutoModel?> Handle(GetProdutoByIdQuery request, CancellationToken cancellationToken)
        {
            var produto = await _context.Produto.
                FirstOrDefaultAsync(produto => (produto.Id == request.Id) &&
                produto.LojaId == request.LojaId,
                cancellationToken);

            return produto;
        }
    }
}
