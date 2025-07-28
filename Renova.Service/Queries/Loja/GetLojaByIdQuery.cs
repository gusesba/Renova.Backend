using MediatR;
using Renova.Domain.Model;

namespace Renova.Service.Queries.Loja
{
    public class GetLojaByIdQuery : IRequest<LojaModel>
    {
        public required Guid Id { get; set; }
    }
}
