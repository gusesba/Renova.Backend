using Renova.Persistence;
using MediatR;
using Renova.Service.Commands.Loja;

namespace Renova.Service.Handlers.Loja
{
    public class DeleteLojaCommandHandler : IRequestHandler<DeleteLojaCommand>
    {
        private readonly RenovaDbContext _context;
        public DeleteLojaCommandHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteLojaCommand request, CancellationToken cancellationToken)
        {
            var loja = await _context.Loja.FindAsync(request.Id, cancellationToken);

            if (loja == null)
            {
                throw new KeyNotFoundException("Loja não encontrada");
            }
            if (loja.UsuarioId != request.UsuarioId)
            {
                throw new UnauthorizedAccessException("Loja não encontrada");
            }
            _context.Loja.Remove(loja);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
