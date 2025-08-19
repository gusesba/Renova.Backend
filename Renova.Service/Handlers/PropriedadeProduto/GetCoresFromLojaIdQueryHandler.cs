using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Persistence;
using Renova.Service.Queries.PropriedadeProduto;

namespace Renova.Service.Handlers.PropriedadeProduto
{
    public class GetCoresFromLojaIdQueryHandler : IRequestHandler<GetCoresFromLojaIdQuery, PagedResult<CorProdutoModel>>
    {
        private readonly RenovaDbContext _context;

        public GetCoresFromLojaIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<CorProdutoModel>> Handle(GetCoresFromLojaIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.CorProduto
                .Where(cor => cor.LojaId == request.LojaId);

            if (!string.IsNullOrWhiteSpace(request.Valor))
            {
                var valorFiltro = request.Valor.ToLower();
                query = query.Where(c => c.Valor.ToLower().Contains(valorFiltro));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            bool ascending = request.OrderDirection?.ToLower() != "desc";
            query = ascending ? query.OrderBy(c => c.Valor) : query.OrderByDescending(c => c.Valor);

            var skip = (request.Page - 1) * request.PageSize;

            var cores = await query
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<CorProdutoModel>
            {
                Items = cores,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                CurrentPage = skip + 1
            };
        }

    }
}
