using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Queries.Loja;

namespace Renova.Service.Handlers.Loja
{
    public class GetLojasFromUsuarioIdQueryHandler : IRequestHandler<GetLojasFromUsuarioIdQuery, List<LojaModel>>
    {
        private readonly RenovaDbContext _context;

        public GetLojasFromUsuarioIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<List<LojaModel>> Handle(GetLojasFromUsuarioIdQuery request, CancellationToken cancellationToken)
        {
            var lojas = await _context.Loja.Where(loja => loja.UsuarioId == request.UsuarioId).ToListAsync(cancellationToken);

            return lojas;
        }
    }
}
