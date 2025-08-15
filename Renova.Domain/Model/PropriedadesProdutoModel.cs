using System.Text.Json.Serialization;

namespace Renova.Domain.Model
{
    public class CorProdutoModel
    {
        public Guid Id { get; set; }
        public string Valor { get; set; }
        [JsonIgnore]
        public Guid? LojaId { get; set; }
        [JsonIgnore]
        public LojaModel Loja { get; set; }
        [JsonIgnore]
        public ICollection<ProdutoModel> Produtos { get; set; }
    }

    public class MarcaProdutoModel
    {
        public Guid Id { get; set; }
        public string Valor { get; set; }
        [JsonIgnore]
        public Guid? LojaId { get; set; }
        [JsonIgnore]
        public LojaModel Loja { get; set; }
        [JsonIgnore]
        public ICollection<ProdutoModel> Produtos { get; set; }
    }

    public class TipoProdutoModel
    {
        public Guid Id { get; set; }
        public string Valor { get; set; }
        [JsonIgnore]
        public Guid? LojaId { get; set; }
        [JsonIgnore]
        public LojaModel Loja { get; set; }
        [JsonIgnore]
        public ICollection<ProdutoModel> Produtos { get; set; }
    }

    public class TamanhoProdutoModel
    {
        public Guid Id { get; set; }
        public string Valor { get; set; }
        [JsonIgnore]
        public Guid? LojaId { get; set; }
        [JsonIgnore]
        public LojaModel Loja { get; set; }
        [JsonIgnore]
        public ICollection<ProdutoModel> Produtos { get; set; }
    }


}
