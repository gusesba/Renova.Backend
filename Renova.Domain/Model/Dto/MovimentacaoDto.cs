using Renova.Domain.Model.Enum;

namespace Renova.Domain.Model.Dto
{
    public class MovimentacaoDto
    {
        public Guid Id { get; set; }
        public TipoMovimentacaoEnum Tipo { get; set; }
        public DateTime Data { get; set; }
        public int TotalItens { get; set; }
        public decimal Valor { get; set; }
        public ICollection<ContasAReceberModel> ContasAReceber { get; set; }
        public ICollection<ProdutoMovimentacaoModel> ProdutoMovimentacoes { get; set; }
    }
}
