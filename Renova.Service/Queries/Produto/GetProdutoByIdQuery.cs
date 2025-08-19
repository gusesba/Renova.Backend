using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Queries.Produto
{
    public class GetProdutoByIdQuery : BaseRequest<ProdutoModel?>
    {
        public Guid? Id { get; set; } = null;
    }
}
