namespace Renova.Domain.Model.Dto
{
    public class DividirContaDto
    {
        public required Guid? MetodoPagamentoId { get; set; }
        public decimal Valor { get; set; }

        public DateTime DataVencimento { get; set; }
    }
}
