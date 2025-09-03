using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Commands.ContasAPagar
{
    public class PagarContasAPagarCommand : BaseRequest<ContasAPagarModel>
    {
        public Guid? Id { get; set; }

        public required Guid? MetodoPagamentoId { get; set; }
    }
}
