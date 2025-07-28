using Renova.Domain.Model;
using Renova.Persistence;
using MediatR;
using Renova.Service.Commands.Cliente;
using Renova.Service.Commands.Auth;
using Renova.Service.Queries.Usuario;

namespace Renova.Service.Handlers.Cliente
{
    public class CriarClienteCommandHandler : IRequestHandler<CriarClienteCommand, ClienteModel>
    {
        private readonly RenovaDbContext _context;
        private readonly IMediator _mediator;
        public CriarClienteCommandHandler(RenovaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ClienteModel> Handle(CriarClienteCommand request, CancellationToken cancellationToken)
        {
            if(request.UsuarioId == null)
            {
                var usuario = await _mediator.Send(new GetUsuarioByEmailQuery() { Email = request.Email });
                if (usuario == null)
                {
                    await _mediator.Send(new SignUpCommand() { Email = request.Email, Nome = request.Nome, Senha = "" });
                    usuario = await _mediator.Send(new GetUsuarioByEmailQuery() { Email = request.Email });
                }
                request.UsuarioId = usuario.Id;
            }

            var cliente = _mediator.Send(new GetClienteByUsuarioId() { UsuarioId = request.UsuarioId });

            if(cliente != null)
            {
                throw new E
            }

            var createdCliente = await _context.Cliente.AddAsync(, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return createdLoja.Entity;
        }
    }
}
