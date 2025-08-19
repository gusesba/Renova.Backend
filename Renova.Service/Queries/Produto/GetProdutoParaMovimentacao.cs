using Renova.Domain.Model;
using Renova.Domain.Model.Enum;
using Renova.Service.Config;

namespace Renova.Service.Queries.Produto
{
    public class GetProdutoParaMovimentacaoQuery : BaseRequest<ProdutoModel?>
    {
        public Guid? Id { get; set; } = null;
        public required TipoMovimentacaoEnum TipoMovimentacao { get; set; }
    }
}
