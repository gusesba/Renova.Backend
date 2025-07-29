using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Queries.Cliente
{
    public class GetClienteByUsuarioIdQuery : BaseRequest<ClienteModel?>
    {
        public required Guid? UsuarioId { get; set; } = null;

    }
}
