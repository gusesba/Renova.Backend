using System.Text.Json.Serialization;

namespace Renova.Domain.Model
{
    public class MovimentacaoModel
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public DateTime Data { get; set; }
        [JsonIgnore]
        public Guid LojaId { get; set; }
        [JsonIgnore]
        public LojaModel Loja { get; set; }
        [JsonIgnore]
        public ICollection<ContasAPagarModel> ContasAPagar { get; set; }
        [JsonIgnore]
        public ICollection<ContasAReceberModel> ContasAReceber { get; set; }
        [JsonIgnore]
        public ICollection<ProdutoMovimentacaoModel> ProdutoMovimentacoes { get; set; }
    }

}
