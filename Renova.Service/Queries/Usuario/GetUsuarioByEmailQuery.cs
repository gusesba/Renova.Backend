using MediatR;
using Renova.Domain.Model;

namespace Renova.Service.Queries.Usuario
{
    public class GetUsuarioByEmailQuery : IRequest<UsuarioModel>
    {
        public required string Email { get; set; } = string.Empty;
    }
}
