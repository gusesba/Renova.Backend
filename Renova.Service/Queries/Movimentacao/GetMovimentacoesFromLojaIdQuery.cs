using Renova.Domain.Model.Dto;
using Renova.Domain.Model.Enum;
using Renova.Domain.Settings;
using Renova.Service.Config;

namespace Renova.Service.Queries.Movimentacao
{
    public class GetMovimentacoesFromLojaIdQuery : BasePaginatedRequest<PagedResult<MovimentacaoDto>>
    {
        public TipoMovimentacaoEnum? Tipo { get; set; }
        public string? Data { get; set; }
        public StatusContaEnum? StatusConta { get; set; }
        public string? TotalItens { get; set; }
        public string? Valor { get; set; }
    }
}
