using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Commands.ContasAReceber
{
    public class PagarContasAReceberCommand : BaseRequest<ContasAReceberModel>
    {
        public Guid? Id { get; set; }

        public required Guid? MetodoPagamentoId { get; set; }
    }
}
