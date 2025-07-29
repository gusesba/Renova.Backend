using Renova.Domain.Model;
using Renova.Persistence;
using MediatR;
using Renova.Service.Commands.Cliente;
using Renova.Service.Commands.Auth;
using Renova.Service.Queries.Usuario;
using System.ComponentModel.DataAnnotations;
using Renova.Service.Queries.Cliente;
using Microsoft.EntityFrameworkCore;


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
            if (request.UsuarioId == null)
            {
                var usuario = await _mediator.Send(new GetUsuarioByEmailQuery() { Email = request.Email });
                if (usuario == null)
                {
                    await _mediator.Send(new SignUpCommand() { Email = request.Email, Nome = request.Nome, Senha = "" });
                    usuario = await _mediator.Send(new GetUsuarioByEmailQuery() { Email = request.Email });
                }
                request.UsuarioId = usuario.Id;
            }

            var cliente = await _mediator.Send(new GetClienteByUsuarioIdQuery() { UsuarioId = request.UsuarioId, LojaId = request.LojaId });

            if (cliente != null)
            {
                throw new ValidationException("Esse usuário já é um cliente");
            }

            var proximaReferencia = await _context.Cliente
                .Where(c => c.LojaId == request.LojaId)
                .MaxAsync(c => (int?)c.Referencia,cancellationToken) ?? 0;

            cliente = new()
            {
                LojaId = request.LojaId,
                Apelido = request.Apelido,
                UsuarioId = request.UsuarioId,
                Referencia = proximaReferencia + 1
            };

            var createdCliente = await _context.Cliente.AddAsync(cliente, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return createdCliente.Entity;
        }
    }
}
