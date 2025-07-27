namespace Renova.Domain.Model
{
    public class TelefoneModel
    {
        public Guid Id { get; set; }
        public string DDI { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }
        public bool Whatsapp { get; set; }

        public Guid UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }

}
