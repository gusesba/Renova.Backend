using MediatR;

namespace Renova.Service.Config
{
    public abstract class BaseRequest<TResponse> : IRequest<TResponse>
    {
        public required Guid? LojaId { get; set; }
    }
}
