using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Domain.Model.Enum;
using Renova.Persistence;
using Renova.Service.Commands.ContasAPagar;

namespace Renova.Service.Handlers.ContasAPagar
{
    public class DividirContasAPagarCommandHandler : IRequestHandler<DividirContasAPagarCommand, ContasAPagarModel>
    {
        private readonly RenovaDbContext _context;
        private readonly IMediator _mediator;
        public DividirContasAPagarCommandHandler(RenovaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ContasAPagarModel> Handle(DividirContasAPagarCommand request, CancellationToken cancellationToken)
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

            contasAPagar.Status = StatusContaEnum.Dividido;

            _context.ContasAPagar.Update(contasAPagar);

            var totParcelas = request.Parcelas.Count;
            for (int i = 0; i < totParcelas; i++)
            {
                var parcela = request.Parcelas[i];

                var novaConta = new ContasAPagarModel
                {
                    LojaId = contasAPagar.LojaId,
                    MovimentacaoId = contasAPagar.MovimentacaoId,
                    OriginalId = contasAPagar.Id,
                    NumParcela = i + 1,
                    TotParcela = totParcelas,
                    Valor = parcela.Valor,
                    MetodoPagamentoId = parcela.MetodoPagamentoId,
                    Status = StatusContaEnum.Pendente,
                    DataVencimento = parcela.DataVencimento,
                    DataPagamento = null,
                };

                await _context.ContasAPagar.AddAsync(novaConta, cancellationToken);
            }

            await _context.SaveChangesAsync();

            return contasAPagar;
        }
    }
}
