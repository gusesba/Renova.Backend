using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Queries.Usuario;

namespace Renova.Service.Handlers.Usuario
{
    public class GetUsuarioByEmailQueryHandler : IRequestHandler<GetUsuarioByEmailQuery, UsuarioModel>
    {
        private readonly RenovaDbContext _context;

        public GetUsuarioByEmailQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioModel> Handle(GetUsuarioByEmailQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            return usuario;
        }
    }
}
