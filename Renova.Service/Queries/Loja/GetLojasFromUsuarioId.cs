using MediatR;
using Renova.Domain.Model;

namespace Renova.Service.Queries.Loja
{
    public class GetLojasFromUsuarioIdQuery : IRequest<List<LojaModel>>
    {
        public required Guid UsuarioId { get; set; }
    }
}
