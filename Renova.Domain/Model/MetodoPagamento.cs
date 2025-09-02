using System.Text.Json.Serialization;

namespace Renova.Domain.Model
{
    public class MetodoPagamento
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Taxa { get; set; }
        [JsonIgnore]
        public ICollection<ContasAPagarModel> ContasAPagar { get; set; }
        [JsonIgnore]
        public ICollection<ContasAReceberModel> ContasAReceber { get; set; }
        public Guid? LojaId { get; set; }
        [JsonIgnore]
        public LojaModel Loja { get; set; }
    }

}
