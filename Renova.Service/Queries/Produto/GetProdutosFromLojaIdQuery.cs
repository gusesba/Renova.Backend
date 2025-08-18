using Renova.Domain.Model;
using Renova.Domain.Model.Enum;
using Renova.Domain.Settings;
using Renova.Service.Config;

namespace Renova.Service.Queries.Produto
{
    public class GetProdutosFromLojaIdQuery : BasePaginatedRequest<PagedResult<ProdutoModel>>
    {
        public int? Referencia {  get; set; }
        public Guid? FornecedorId { get; set; }
        public string? Preco {  get; set; }
        public StatusProdutoEnum? Status { get; set; }
        public Guid? CorId { get; set; }
        public Guid? TamanhoId { get; set; }
        public Guid? TipoId { get; set; }
        public string? Descricao { get; set; }
        public Guid? MarcaId { get; set; }
        public string? DataEntrada { get; set; }
    }
}
