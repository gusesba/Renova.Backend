using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Queries.Loja;

namespace Renova.Service.Handlers.Loja
{
    public class GetLojaByIdQueryHandler : IRequestHandler<GetLojaByIdQuery, LojaModel>
    {
        private readonly RenovaDbContext _context;

        public GetLojaByIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<LojaModel> Handle(GetLojaByIdQuery request, CancellationToken cancellationToken)
        {
            var loja = await _context.Loja.FirstOrDefaultAsync(loja => loja.Id == request.Id, cancellationToken);

            return loja;
        }
    }
}
