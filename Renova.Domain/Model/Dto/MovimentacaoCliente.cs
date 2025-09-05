using Renova.Domain.Model.Enum;

namespace Renova.Domain.Model.Dto
{
    public class MovimentacaoClienteDto
    {
        public Guid Id { get; set; }
        public TipoMovimentacaoEnum Tipo { get; set; }
        public DateTime Data { get; set; }
        public Guid? ClienteId { get; set; }
        public ClienteModel Cliente { get; set; }
    }
}
