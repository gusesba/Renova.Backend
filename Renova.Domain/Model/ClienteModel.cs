namespace Renova.Domain.Model
{
    public class ClienteModel
    {
        public Guid Id { get; set; }
        public int Referencia { get; set; }
        public string Nome { get; set; }

        public Guid UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }

        public Guid LojaId { get; set; }
        public LojaModel Loja { get; set; }

        // Relacionamentos
        public ICollection<ProdutoModel> ProdutosFornecidos { get; set; }
    }
}
