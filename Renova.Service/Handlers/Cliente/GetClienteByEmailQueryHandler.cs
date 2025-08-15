using Renova.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Service.Queries.Cliente;

namespace Renova.Service.Handlers.Cliente
{
    public class GetClienteByEmailQueryHandler : IRequestHandler<GetClienteByEmailQuery,ClienteModel?>
    {
        private readonly RenovaDbContext _context;

        public GetClienteByEmailQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<ClienteModel?> Handle(GetClienteByEmailQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _context.Cliente.Include(cliente => cliente.Usuario).
                FirstOrDefaultAsync(cliente => (cliente.Usuario.Email == request.Email) &&
                cliente.LojaId == request.LojaId,
                cancellationToken);

            return cliente;
        }
    }
}
