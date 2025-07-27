namespace Renova.Domain.Model
{
    public class ProdutoMovimentacaoModel
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }

        public Guid ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }

        public Guid MovimentacaoId { get; set; }
        public MovimentacaoModel Movimentacao { get; set; }

        public Guid LojaId { get; set; }
        public LojaModel Loja { get; set; }
    }

}
