using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Queries.Cliente
{
    public class GetClienteByIdQuery : BaseRequest<ClienteModel?>
    {
        public Guid? Id { get; set; } = null;

    }
}
