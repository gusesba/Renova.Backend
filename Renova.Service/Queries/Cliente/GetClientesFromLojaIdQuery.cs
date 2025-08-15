using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Config;

namespace Renova.Service.Queries.Cliente
{
    public class GetClientesFromLojaIdQuery : BasePaginatedRequest<PagedResult<ClienteModel>>
    {
        public string? Nome { get; set; }
        public string? Apelido { get; set; }
        public string? Email { get; set; }
        public int? Referencia { get; set; }
    }
}
