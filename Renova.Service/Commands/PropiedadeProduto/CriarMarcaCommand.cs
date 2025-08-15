using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Commands.PropriedadeProduto
{
    public class CriarMarcaCommand : BaseRequest<MarcaProdutoModel>
    {
        public required string Valor { get; set; }
    }
}
