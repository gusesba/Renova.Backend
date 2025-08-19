using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Queries.Cliente;

namespace Renova.Service.Handlers.Cliente
{
    public class GetClienteByUsuarioIdQueryHandler : IRequestHandler<GetClienteByUsuarioIdQuery, ClienteModel?>
    {
        private readonly RenovaDbContext _context;

        public GetClienteByUsuarioIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<ClienteModel?> Handle(GetClienteByUsuarioIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _context.Cliente.
                FirstOrDefaultAsync(cliente =>
                (cliente.UsuarioId == request.UsuarioId) &&
                (cliente.LojaId == request.LojaId),
                cancellationToken);

            return cliente;
        }
    }
}
