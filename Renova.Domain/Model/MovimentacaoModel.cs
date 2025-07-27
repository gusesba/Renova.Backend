namespace Renova.Domain.Model
{
    public class MovimentacaoModel
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public DateTime Data { get; set; }

        public Guid LojaId { get; set; }
        public LojaModel Loja { get; set; }

        public ICollection<ContasAPagarModel> ContasAPagar { get; set; }
        public ICollection<ContasAReceberModel> ContasAReceber { get; set; }
        public ICollection<ProdutoMovimentacaoModel> ProdutoMovimentacoes { get; set; }
    }

}
