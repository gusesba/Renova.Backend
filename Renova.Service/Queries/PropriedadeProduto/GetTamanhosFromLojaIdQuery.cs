using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Config;

namespace Renova.Service.Queries.PropriedadeProduto
{
    public class GetTamanhosFromLojaIdQuery : BasePaginatedRequest<PagedResult<TamanhoProdutoModel>>
    {
        public string? Valor { get; set; }
    }
}
