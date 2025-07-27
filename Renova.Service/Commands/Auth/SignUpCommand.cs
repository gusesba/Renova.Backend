using Renova.Domain.Model.Dto;
using MediatR;

namespace Renova.Service.Commands.Auth
{
    public class SignUpCommand : IRequest<LoginDto>
    {
        public required string Email { get; set; } = string.Empty;
        public required string Senha { get; set; } = string.Empty;
        public required string Nome { get; set; } = string.Empty;
    }
}
