using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Domain.Model.Enum;
using Renova.Persistence;
using Renova.Service.Commands.ContasAPagar;

namespace Renova.Service.Handlers.ContasAPagar
{
    public class PagarContasAPagarCommandHandler : IRequestHandler<PagarContasAPagarCommand, ContasAPagarModel>
    {
        private readonly RenovaDbContext _context;
        private readonly IMediator _mediator;
        public PagarContasAPagarCommandHandler(RenovaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ContasAPagarModel> Handle(PagarContasAPagarCommand request, CancellationToken cancellationToken)
        {
            var contasAPagar = await _context.ContasAPagar.Where(c => c.LojaId == request.LojaId && c.Id == request.Id).FirstOrDefaultAsync();

            if (contasAPagar == null)
            {
                throw new KeyNotFoundException("ContaAPagar não encontrada");
            }

            if (contasAPagar.Status != StatusContaEnum.Pendente)
            {
                throw new ValidationException($"Operação Inválida, Status da conta: {contasAPagar.Status}");
            }

            contasAPagar.Status = StatusContaEnum.Pago;
            contasAPagar.MetodoPagamentoId = request.MetodoPagamentoId;
            contasAPagar.DataPagamento = DateTime.UtcNow;

            _context.ContasAPagar.Update(contasAPagar);
            await _context.SaveChangesAsync();

            return contasAPagar;

        }
    }
}
