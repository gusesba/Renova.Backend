using Renova.Persistence;
using MediatR;
using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Queries.PropriedadeProduto;
using Microsoft.EntityFrameworkCore;

namespace Renova.Service.Handlers.PropriedadeProduto
{
    public class GetMarcasFromLojaIdQueryHandler : IRequestHandler<GetMarcasFromLojaIdQuery, PagedResult<MarcaProdutoModel>>
    {
        private readonly RenovaDbContext _context;

        public GetMarcasFromLojaIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<MarcaProdutoModel>> Handle(GetMarcasFromLojaIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.MarcaProduto
                .Where(marca => marca.LojaId == request.LojaId);

            if (!string.IsNullOrWhiteSpace(request.Valor))
            {
                var valorFiltro = request.Valor.ToLower();
                query = query.Where(c => c.Valor.ToLower().Contains(valorFiltro));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            bool ascending = request.OrderDirection?.ToLower() != "desc";
            query = ascending ? query.OrderBy(c => c.Valor) : query.OrderByDescending(c => c.Valor);

            var skip = (request.Page - 1) * request.PageSize;

            var marcas = await query
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<MarcaProdutoModel>
            {
                Items = marcas,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                CurrentPage = skip + 1
            };
        }

    }
}
