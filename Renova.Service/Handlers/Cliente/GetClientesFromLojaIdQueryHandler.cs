using Renova.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Service.Queries.Cliente;

namespace Renova.Service.Handlers.Cliente
{
    public class GetClientesFromLojaIdQueryHandler : IRequestHandler<GetClientesFromLojaIdQuery, List<ClienteModel>>
    {
        private readonly RenovaDbContext _context;

        public GetClientesFromLojaIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteModel>> Handle(GetClientesFromLojaIdQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _context.Cliente.
                Include(cliente=>cliente.Usuario).
                Where(cliente => cliente.LojaId  == request.LojaId).
                ToListAsync();

            return clientes;
        }
    }
}
