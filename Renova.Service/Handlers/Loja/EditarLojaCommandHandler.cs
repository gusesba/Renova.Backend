using Renova.Domain.Model;
using Renova.Persistence;
using MediatR;
using Renova.Service.Commands.Loja;

namespace Renova.Service.Handlers.Loja
{
    public class EditarLojaCommandHandler : IRequestHandler<EditarLojaCommand, LojaModel>
    {
        private readonly RenovaDbContext _context;
        public EditarLojaCommandHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<LojaModel> Handle(EditarLojaCommand request, CancellationToken cancellationToken)
        {
            var loja = await _context.Loja.FindAsync(request.Id, cancellationToken);

            if (loja == null)
            {
                throw new KeyNotFoundException("Loja não encontrada");
            }
            if(loja.UsuarioId != request.UsuarioId)
            {
                throw new UnauthorizedAccessException("Loja não encontrada");
            }

            if(request.Nome != null)
            {
                loja.Nome = request.Nome;
            }

            var updatedLoja = _context.Loja.Update(loja);
            await _context.SaveChangesAsync(cancellationToken);

            return updatedLoja.Entity;
        }
    }
}
