using Renova.Domain.Model;
using Renova.Domain.Model.Enum;
using Renova.Domain.Settings;
using Renova.Service.Config;

namespace Renova.Service.Queries.ContasAReceber
{
    public class GetContasAReceberFromLojaIdQuery : BasePaginatedRequest<PagedResult<ContasAReceberModel>>
    {
        public string? Valor { get; set; }
        public StatusContaEnum? Status { get; set; }
        public string? NumParcela { get; set; }
        public string? TotParcela { get; set; }
        public string? DataVencimento { get; set; }
        public string? DataPagamento { get; set; }
        public string? MetodoPagamentoNome { get; set; }
        public string? MetodoPagamentoTaxa { get; set; }
        public Guid? ClienteId { get; set; }
    }
}
