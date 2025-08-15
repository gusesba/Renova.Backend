using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Commands.PropriedadeProduto
{
    public class CriarTipoCommand : BaseRequest<TipoProdutoModel>
    {
        public required string Valor { get; set; }
    }
}
