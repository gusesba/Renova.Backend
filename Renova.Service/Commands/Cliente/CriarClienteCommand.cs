using MediatR;
using Renova.Domain.Model;

namespace Renova.Service.Commands.Cliente
{
    public class CriarClienteCommand : IRequest<ClienteModel>
    {
        public string? Apelido { get; set; } = null;
        public Guid? UsuarioId { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Nome { get; set; } = null;
        public required Guid? LojaId { get; set; } = null;
    }
}
