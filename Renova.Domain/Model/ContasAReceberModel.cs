using System.Text.Json.Serialization;

namespace Renova.Domain.Model
{
    public class ContasAReceberModel
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }
        public Guid? OriginalId { get; set; }
        public ContasAReceberModel? Original { get; set; }
        public int NumParcela { get; set; }
        public int TotParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }

        public Guid MovimentacaoId { get; set; }
        public MovimentacaoModel Movimentacao { get; set; }
        [JsonIgnore]
        public Guid LojaId { get; set; }
        [JsonIgnore]
        public LojaModel Loja { get; set; }
    }

}
