using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Commands.Cliente
{
    public class CriarClienteCommand : BaseRequest<ClienteModel>
    {
        public string? Apelido { get; set; } = null;
        public Guid? UsuarioId { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Nome { get; set; } = null;
    }
}
