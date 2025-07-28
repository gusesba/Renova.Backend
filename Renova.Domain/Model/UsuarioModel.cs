using System.Text.Json.Serialization;

namespace Renova.Domain.Model
{
    public class UsuarioModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string SenhaHash { get; set; }

        // Relacionamentos
        [JsonIgnore]
        public ICollection<TelefoneModel> Telefones { get; set; }
        [JsonIgnore]
        public ICollection<LojaModel> Lojas { get; set; }
        [JsonIgnore]
        public ICollection<ClienteModel> Clientes { get; set; }
    }

}
