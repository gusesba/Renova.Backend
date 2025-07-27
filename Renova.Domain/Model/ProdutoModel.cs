namespace Renova.Domain.Model
{
    public class ProdutoModel
    {
        public Guid Id { get; set; }
        public int Referencia { get; set; }
        public decimal Preco { get; set; }
        public string Status { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEntrada { get; set; }

        public Guid FornecedorId { get; set; }
        public ClienteModel Fornecedor { get; set; }

        public Guid CorId { get; set; }
        public CorProdutoModel Cor { get; set; }

        public Guid TamanhoId { get; set; }
        public TamanhoProdutoModel Tamanho { get; set; }

        public Guid MarcaId { get; set; }
        public MarcaProdutoModel Marca { get; set; }

        public Guid TipoId { get; set; }
        public TipoProdutoModel Tipo { get; set; }

        public Guid LojaId { get; set; }
        public LojaModel Loja { get; set; }

        public ICollection<ProdutoMovimentacaoModel> Movimentacoes { get; set; }
    }

}
