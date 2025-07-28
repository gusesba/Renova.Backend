using MediatR;

namespace Renova.Service.Commands.Loja
{
    public class DeleteLojaCommand : IRequest
    {
        public required Guid Id { get; set; } = Guid.Empty;

        public Guid UsuarioId { get; set; } = Guid.Empty;
    }
}
