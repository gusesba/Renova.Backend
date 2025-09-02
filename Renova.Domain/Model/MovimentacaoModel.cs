using System.Text.Json.Serialization;
using Renova.Domain.Model.Enum;

namespace Renova.Domain.Model
{
    public class MovimentacaoModel
    {
        public Guid Id { get; set; }
        public TipoMovimentacaoEnum Tipo { get; set; }
        public DateTime Data { get; set; }
        public Guid? ClienteId { get; set; }
        [JsonIgnore]
        public ClienteModel Cliente { get; set; }
        [JsonIgnore]
        public Guid? LojaId { get; set; }
        [JsonIgnore]
        public LojaModel Loja { get; set; }
        [JsonIgnore]
        public ICollection<ContasAPagarModel> ContasAPagar { get; set; }
        public ICollection<ContasAReceberModel> ContasAReceber { get; set; }
        public ICollection<ProdutoMovimentacaoModel> ProdutoMovimentacoes { get; set; }
    }

}
