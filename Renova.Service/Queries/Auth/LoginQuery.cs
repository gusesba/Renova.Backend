using Renova.Domain.Model.Dto;
using MediatR;

namespace Renova.Service.Queries.Auth
{
    public class LoginQuery : IRequest<LoginDto>
    {
        public required string Email { get; set; } = string.Empty;
        public required string Senha { get; set; } = string.Empty;
    }
}
