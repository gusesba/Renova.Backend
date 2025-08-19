using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Persistence;
using Renova.Service.Queries.PropriedadeProduto;

namespace Renova.Service.Handlers.PropriedadeProduto
{
    public class GetTamanhosFromLojaIdQueryHandler : IRequestHandler<GetTamanhosFromLojaIdQuery, PagedResult<TamanhoProdutoModel>>
    {
        private readonly RenovaDbContext _context;

        public GetTamanhosFromLojaIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TamanhoProdutoModel>> Handle(GetTamanhosFromLojaIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.TamanhoProduto
                .Where(tamanho => tamanho.LojaId == request.LojaId);

            if (!string.IsNullOrWhiteSpace(request.Valor))
            {
                var valorFiltro = request.Valor.ToLower();
                query = query.Where(c => c.Valor.ToLower().Contains(valorFiltro));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            bool ascending = request.OrderDirection?.ToLower() != "desc";
            query = ascending ? query.OrderBy(c => c.Valor) : query.OrderByDescending(c => c.Valor);

            var skip = (request.Page - 1) * request.PageSize;

            var tamanhos = await query
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<TamanhoProdutoModel>
            {
                Items = tamanhos,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                CurrentPage = skip + 1
            };
        }

    }
}
