using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Commands.Cliente
{
    public class EditarClienteCommand : BaseRequest<ClienteModel>
    {
        public Guid? Id { get; set; } = null; 
        public string? Apelido { get; set; } = null;
        public Guid? UsuarioId { get; set; } = null;
    }
}
