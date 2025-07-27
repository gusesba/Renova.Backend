namespace Renova.Domain.Model
{
    public class CorProdutoModel
    {
        public Guid Id { get; set; }
        public string Valor { get; set; }

        public Guid LojaId { get; set; }
        public LojaModel Loja { get; set; }

        public ICollection<ProdutoModel> Produtos { get; set; }
    }

    public class MarcaProdutoModel
    {
        public Guid Id { get; set; }
        public string Valor { get; set; }

        public Guid LojaId { get; set; }
        public LojaModel Loja { get; set; }

        public ICollection<ProdutoModel> Produtos { get; set; }
    }

    public class TipoProdutoModel
    {
        public Guid Id { get; set; }
        public string Valor { get; set; }

        public Guid LojaId { get; set; }
        public LojaModel Loja { get; set; }

        public ICollection<ProdutoModel> Produtos { get; set; }
    }

    public class TamanhoProdutoModel
    {
        public Guid Id { get; set; }
        public string Valor { get; set; }

        public Guid LojaId { get; set; }
        public LojaModel Loja { get; set; }

        public ICollection<ProdutoModel> Produtos { get; set; }
    }


}
