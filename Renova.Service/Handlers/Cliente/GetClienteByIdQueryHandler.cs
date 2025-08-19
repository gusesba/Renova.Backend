using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Queries.Cliente;

namespace Renova.Service.Handlers.Cliente
{
    public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, ClienteModel?>
    {
        private readonly RenovaDbContext _context;

        public GetClienteByIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<ClienteModel?> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _context.Cliente.Include(cliente => cliente.Usuario).
                FirstOrDefaultAsync(cliente => (cliente.Id == request.Id) &&
                cliente.LojaId == request.LojaId,
                cancellationToken);

            return cliente;
        }
    }
}
