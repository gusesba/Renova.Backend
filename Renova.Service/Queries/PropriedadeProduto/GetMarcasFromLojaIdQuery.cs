using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Config;

namespace Renova.Service.Queries.PropriedadeProduto
{
    public class GetMarcasFromLojaIdQuery : BasePaginatedRequest<PagedResult<MarcaProdutoModel>>
    {
        public string? Valor { get; set; }
    }
}
