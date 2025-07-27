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

        public Guid LojaId { get; set; }
        public LojaModel Loja { get; set; }
    }

}
