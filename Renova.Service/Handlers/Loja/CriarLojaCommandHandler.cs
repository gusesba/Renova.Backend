using MediatR;
using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Commands.Loja;

namespace Renova.Service.Handlers.Loja
{
    public class CriarLojaCommandHandler : IRequestHandler<CriarLojaCommand, LojaModel>
    {
        private readonly RenovaDbContext _context;
        public CriarLojaCommandHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<LojaModel> Handle(CriarLojaCommand request, CancellationToken cancellationToken)
        {
            var lojaModel = new LojaModel
            {
                Nome = request.Nome,
                UsuarioId = request.UsuarioId
            };
            var createdLoja = await _context.Loja.AddAsync(lojaModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return createdLoja.Entity;
        }
    }
}
