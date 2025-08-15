using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Commands.PropriedadeProduto
{
    public class CriarCorCommand : BaseRequest<CorProdutoModel>
    {
        public required string Valor { get; set; }
    }
}
