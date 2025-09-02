using System.Text.Json.Serialization;

namespace Renova.Domain.Model
{
    public class LojaModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public Guid UsuarioId { get; set; }

        public UsuarioModel Usuario { get; set; }

        // Relacionamentos
        [JsonIgnore]
        public ICollection<ClienteModel> Clientes { get; set; }
        [JsonIgnore]
        public ICollection<ProdutoModel> Produtos { get; set; }
        [JsonIgnore]
        public ICollection<CorProdutoModel> Cores { get; set; }
        [JsonIgnore]
        public ICollection<TamanhoProdutoModel> Tamanhos { get; set; }
        [JsonIgnore]
        public ICollection<MarcaProdutoModel> Marcas { get; set; }
        [JsonIgnore]
        public ICollection<TipoProdutoModel> Tipos { get; set; }
        [JsonIgnore]
        public ICollection<MovimentacaoModel> Movimentacoes { get; set; }
        [JsonIgnore]
        public ICollection<ContasAPagarModel> ContasAPagar { get; set; }
        [JsonIgnore]
        public ICollection<ContasAReceberModel> ContasAReceber { get; set; }
        [JsonIgnore]
        public ICollection<ProdutoMovimentacaoModel> ProdutoMovimentacoes { get; set; }
        [JsonIgnore]
        public ICollection<MetodoPagamento> MetodosPagamento { get; set; }
    }

}
