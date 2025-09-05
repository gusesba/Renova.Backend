using Renova.Domain.Model;
using Renova.Domain.Model.Dto;
using Renova.Service.Config;

namespace Renova.Service.Commands.ContasAPagar
{
    public class DividirContasAPagarCommand : BaseRequest<ContasAPagarModel>
    {
        public Guid? Id { get; set; }

        public List<DividirContaDto> Parcelas { get; set; }
    }
}
