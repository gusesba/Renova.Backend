using Renova.Domain.Model;
using Renova.Domain.Model.Dto;
using Renova.Domain.Model.Enum;
using Renova.Service.Config;

namespace Renova.Service.Commands.Movimentacao
{
    public class CriarMovimentacaoCommand : BaseRequest<MovimentacaoModel>
    {
        public List<ProdutoValorDto> Produtos { get; set; }
        public TipoMovimentacaoEnum? Tipo { get; set; }
        public Guid? ClienteID { get; set; }
        public DateTime? Data { get; set; }
    }
}
