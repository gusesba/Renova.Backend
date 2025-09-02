using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Commands.MetodoPagamento
{
    public class CriarMetodoPagamentoCommand : BaseRequest<MetodoPagamentoModel>
    {
        public required string? Nome { get; set; }
        public required decimal? Taxa { get; set; }
    }
}
