using System.Text.Json.Serialization;

namespace Renova.Domain.Model
{
    public class ClienteModel
    {
        public Guid Id { get; set; }
        public int Referencia { get; set; }
        public string? Apelido { get; set; }

        public Guid? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
        [JsonIgnore]
        public Guid? LojaId { get; set; }
        [JsonIgnore]
        public LojaModel Loja { get; set; }

        // Relacionamentos
        [JsonIgnore]
        public ICollection<ProdutoModel> ProdutosFornecidos { get; set; }
    }
}
