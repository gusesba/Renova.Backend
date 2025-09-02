using MediatR;
using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Commands.MetodoPagamento;

namespace Renova.Service.Handlers.MetodoPagamento
{
    public class CriarMetodoPagamentoCommandHandler : IRequestHandler<CriarMetodoPagamentoCommand, MetodoPagamentoModel>
    {
        private readonly RenovaDbContext _context;
        public CriarMetodoPagamentoCommandHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<MetodoPagamentoModel> Handle(CriarMetodoPagamentoCommand request, CancellationToken cancellationToken)
        {
            var metodoPagamento = new MetodoPagamentoModel()
            {
                Nome = request.Nome,
                Taxa = request.Taxa,
                LojaId = request.LojaId
            };

            var createdMetodoPagamento = await _context.MetodoPagamento.AddAsync(metodoPagamento, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return createdMetodoPagamento.Entity;
        }
    }
}
