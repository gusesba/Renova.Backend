using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Config;

namespace Renova.Service.Queries.PropriedadeProduto
{
    public class GetCoresFromLojaIdQuery : BasePaginatedRequest<PagedResult<CorProdutoModel>>
    {
        public string? Valor { get; set; }
    }
}
