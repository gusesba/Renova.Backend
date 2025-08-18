using Renova.Domain.Model;
using Renova.Domain.Model.Enum;
using Renova.Service.Config;

namespace Renova.Service.Commands.Produto
{
    public class CriarProdutoCommand : BaseRequest<ProdutoModel>
    {
        public required Guid? FornecedorId { get; set; }
        public required decimal? Preco { get; set; }
        public StatusProdutoEnum? Status {  get; set; }
        public required Guid? CorId { get; set; }
        public required Guid? TamanhoId { get; set; }
        public required Guid? MarcaId { get; set; }
        public required Guid? TipoId { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataEntrada { get; set; }
    }
}
