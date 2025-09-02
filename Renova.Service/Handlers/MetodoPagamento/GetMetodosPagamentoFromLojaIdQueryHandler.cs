using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Persistence;
using Renova.Service.Queries.MetodoPagamento;

namespace Renova.Service.Handlers.MetodoPagamento
{
    public class GetMetodosPagamentoFromLojaIdQueryHandler : IRequestHandler<GetMetodosPagamentoFromLojaIdQuery, PagedResult<MetodoPagamentoModel>>
    {
        private readonly RenovaDbContext _context;

        public GetMetodosPagamentoFromLojaIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<MetodoPagamentoModel>> Handle(GetMetodosPagamentoFromLojaIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.MetodoPagamento
                .Where(produto => produto.LojaId == request.LojaId);

            var totalCount = await query.CountAsync(cancellationToken);

            bool ascending = request.OrderDirection?.ToLower() != "desc";
            query = request.OrderBy?.ToLower() switch
            {
                "taxa" => ascending ? query.OrderBy(c => c.Taxa) : query.OrderByDescending(c => c.Taxa),
                "nome" or _ => ascending ? query.OrderBy(c => c.Nome) : query.OrderByDescending(c => c.Nome),
            };

            var skip = (request.Page - 1) * request.PageSize;

            var metodosPagamentoModels = await query
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<MetodoPagamentoModel>
            {
                Items = metodosPagamentoModels,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                CurrentPage = skip + 1
            };
        }
    }
}
