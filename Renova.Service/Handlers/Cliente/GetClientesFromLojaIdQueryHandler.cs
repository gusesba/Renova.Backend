using Renova.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Service.Queries.Cliente;
using Renova.Domain.Settings;

namespace Renova.Service.Handlers.Cliente
{
    public class GetClientesFromLojaIdQueryHandler : IRequestHandler<GetClientesFromLojaIdQuery, PagedResult<ClienteModel>>
    {
        private readonly RenovaDbContext _context;

        public GetClientesFromLojaIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ClienteModel>> Handle(GetClientesFromLojaIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Cliente
                .Include(cliente => cliente.Usuario)
                .Where(cliente => cliente.LojaId == request.LojaId);

            if (!string.IsNullOrWhiteSpace(request.Nome))
            {
                var nomeFiltro = request.Nome.ToLower();
                query = query.Where(c => c.Usuario.Nome.ToLower().Contains(nomeFiltro));
            }
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                var emailFiltro = request.Email.ToLower();
                query = query.Where(c => c.Usuario.Email.ToLower().Contains(emailFiltro));
            }
            if (!string.IsNullOrWhiteSpace(request.Apelido))
            {
                var apelidoFiltro = request.Apelido.ToLower();
                query = query.Where(c => c.Apelido != null && c.Apelido.ToLower().Contains(apelidoFiltro));
            }
            if (request.Referencia != null)
            {
                query = query.Where(c => c.Referencia == request.Referencia);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            bool ascending = request.OrderDirection?.ToLower() != "desc";
            query = request.OrderBy?.ToLower() switch
            {
                "email" => ascending ? query.OrderBy(c => c.Usuario.Email) : query.OrderByDescending(c => c.Usuario.Email),
                "nome" => ascending ? query.OrderBy(c => c.Usuario.Nome) : query.OrderByDescending(c => c.Usuario.Nome),
                "apelido" => ascending ? query.OrderBy(c => c.Apelido) : query.OrderByDescending(c => c.Apelido),
                "referencia" or _ => ascending ? query.OrderBy(c=>c.Referencia) : query.OrderByDescending(c => c.Referencia),
            };

            var skip = (request.Page - 1) * request.PageSize;

            var clientes = await query
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<ClienteModel>
            {
                Items = clientes,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
        }

    }
}
