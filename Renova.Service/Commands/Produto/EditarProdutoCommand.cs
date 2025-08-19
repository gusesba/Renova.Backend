using Renova.Domain.Model;
using Renova.Domain.Model.Enum;
using Renova.Service.Config;

namespace Renova.Service.Commands.Produto
{
    public class EditarProdutoCommand : BaseRequest<ProdutoModel>
    {
        public Guid? Id { get; set; }
        public Guid? FornecedorId { get; set; }
        public decimal? Preco { get; set; }
        public StatusProdutoEnum? Status { get; set; }
        public Guid? CorId { get; set; }
        public Guid? TamanhoId { get; set; }
        public Guid? MarcaId { get; set; }
        public Guid? TipoId { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataEntrada { get; set; }
    }
}
