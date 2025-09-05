using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Domain.Model.Enum;
using Renova.Persistence;
using Renova.Service.Commands.ContasAReceber;

namespace Renova.Service.Handlers.ContasAReceber
{
    public class PagarContasAReceberCommandHandler : IRequestHandler<PagarContasAReceberCommand, ContasAReceberModel>
    {
        private readonly RenovaDbContext _context;
        private readonly IMediator _mediator;
        public PagarContasAReceberCommandHandler(RenovaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ContasAReceberModel> Handle(PagarContasAReceberCommand request, CancellationToken cancellationToken)
        {
            var contasAReceber = await _context.ContasAReceber.Where(c => c.LojaId == request.LojaId && c.Id == request.Id).FirstOrDefaultAsync();

            if (contasAReceber == null)
            {
                throw new KeyNotFoundException("ContaAReceber não encontrada");
            }

            if (contasAReceber.Status != StatusContaEnum.Pendente)
            {
                throw new ValidationException($"Operação Inválida, Status da conta: {contasAReceber.Status}");
            }

            contasAReceber.Status = StatusContaEnum.Pago;
            contasAReceber.MetodoPagamentoId = request.MetodoPagamentoId;
            contasAReceber.DataPagamento = DateTime.UtcNow;

            _context.ContasAReceber.Update(contasAReceber);
            await _context.SaveChangesAsync();

            return contasAReceber;

        }
    }
}
