using MediatR;
using Renova.Domain.Model;

namespace Renova.Service.Commands.Loja
{
    public class EditarLojaCommand : IRequest<LojaModel>
    {
        public string? Nome { get; set; } = null;
        public Guid Id { get; set; } = Guid.Empty;
        public Guid UsuarioId { get; set; } = Guid.Empty;

    }
}
