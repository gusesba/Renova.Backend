using Renova.Persistence;
using MediatR;
using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Queries.PropriedadeProduto;
using Microsoft.EntityFrameworkCore;

namespace Renova.Service.Handlers.PropriedadeProduto
{
    public class GetTiposFromLojaIdQueryHandler : IRequestHandler<GetTiposFromLojaIdQuery, PagedResult<TipoProdutoModel>>
    {
        private readonly RenovaDbContext _context;

        public GetTiposFromLojaIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TipoProdutoModel>> Handle(GetTiposFromLojaIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.TipoProduto
                .Where(tipo => tipo.LojaId == request.LojaId);

            if (!string.IsNullOrWhiteSpace(request.Valor))
            {
                var valorFiltro = request.Valor.ToLower();
                query = query.Where(c => c.Valor.ToLower().Contains(valorFiltro));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            bool ascending = request.OrderDirection?.ToLower() != "desc";
            query = ascending ? query.OrderBy(c => c.Valor) : query.OrderByDescending(c => c.Valor);

            var skip = (request.Page - 1) * request.PageSize;

            var tipos = await query
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<TipoProdutoModel>
            {
                Items = tipos,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                CurrentPage = skip + 1
            };
        }

    }
}
