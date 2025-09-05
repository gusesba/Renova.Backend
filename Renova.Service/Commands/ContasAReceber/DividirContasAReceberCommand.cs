using Renova.Domain.Model;
using Renova.Domain.Model.Dto;
using Renova.Service.Config;

namespace Renova.Service.Commands.ContasAReceber
{
    public class DividirContasAReceberCommand : BaseRequest<ContasAReceberModel>
    {
        public Guid? Id { get; set; }

        public List<DividirContaDto> Parcelas { get; set; }
    }
}
