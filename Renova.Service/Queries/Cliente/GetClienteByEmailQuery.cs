using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Queries.Cliente
{
    public class GetClienteByEmailQuery : BaseRequest<ClienteModel?>
    {
        public string? Email { get; set; } = null;

    }
}
