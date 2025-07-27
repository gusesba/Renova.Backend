using MediatR;
using Renova.Domain.Model;

namespace Renova.Service.Commands.Loja
{
    public class CriarLojaCommand : IRequest<LojaModel>
    {
        public required string Nome { get; set; } = string.Empty;

        public Guid UsuarioId { get; set; } = Guid.Empty;
    }
}
