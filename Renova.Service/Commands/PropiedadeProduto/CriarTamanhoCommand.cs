using Renova.Domain.Model;
using Renova.Service.Config;

namespace Renova.Service.Commands.PropriedadeProduto
{
    public class CriarTamanhoCommand : BaseRequest<TamanhoProdutoModel>
    {
        public required string Valor { get; set; }
    }
}
