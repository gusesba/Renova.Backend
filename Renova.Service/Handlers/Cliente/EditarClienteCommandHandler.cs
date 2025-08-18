using Renova.Domain.Model;
using Renova.Persistence;
using MediatR;
using Renova.Service.Commands.Cliente;
using System.ComponentModel.DataAnnotations;
using Renova.Service.Queries.Cliente;


namespace Renova.Service.Handlers.Cliente
{
    public class EditarClienteCommandHandler : IRequestHandler<EditarClienteCommand, ClienteModel>
    {
        private readonly RenovaDbContext _context;
        private readonly IMediator _mediator;
        public EditarClienteCommandHandler(RenovaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ClienteModel> Handle(EditarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _mediator.Send(new GetClienteByIdQuery() { Id = request.Id, LojaId = request.LojaId });

            if (cliente == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado");
            }
            
            if(request.Apelido != null)
            {
                cliente.Apelido = request.Apelido;
            }
            if(request.UsuarioId != null)
            {
                var existeCliente = await _mediator.Send(new GetClienteByUsuarioIdQuery() { UsuarioId = request.UsuarioId, LojaId = request.LojaId });

                if(existeCliente != null)
                {
                    throw new ValidationException("O usuário já está atrelado a outro cliente");
                }

                cliente.UsuarioId = request.UsuarioId;
            }

            _context.Cliente.Update(cliente);
            await _context.SaveChangesAsync();

            cliente.Usuario = null;

            return cliente;

        }
    }
}
