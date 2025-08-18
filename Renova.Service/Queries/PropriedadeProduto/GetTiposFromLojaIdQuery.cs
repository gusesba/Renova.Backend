using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Config;

namespace Renova.Service.Queries.PropriedadeProduto
{
    public class GetTiposFromLojaIdQuery : BasePaginatedRequest<PagedResult<TipoProdutoModel>>
    {
        public string? Valor { get; set; }
    }
}
