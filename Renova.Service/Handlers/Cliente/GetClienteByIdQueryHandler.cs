using Renova.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Service.Queries.Cliente;

namespace Renova.Service.Handlers.Loja
{
    public class GetClienteByUsuarioIdQueryHandler : IRequestHandler<GetClienteByIdQuery,ClienteModel?>
    {
        private readonly RenovaDbContext _context;

        public GetClienteByUsuarioIdQueryHandler(RenovaDbContext context)
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
