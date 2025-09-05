using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Domain.Model.Enum;
using Renova.Persistence;
using Renova.Service.Commands.ContasAReceber;

namespace Renova.Service.Handlers.ContasAReceber
{
    public class DividirContasAReceberCommandHandler : IRequestHandler<DividirContasAReceberCommand, ContasAReceberModel>
    {
        private readonly RenovaDbContext _context;
        private readonly IMediator _mediator;
        public DividirContasAReceberCommandHandler(RenovaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ContasAReceberModel> Handle(DividirContasAReceberCommand request, CancellationToken cancellationToken)
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

            contasAReceber.Status = StatusContaEnum.Dividido;

            _context.ContasAReceber.Update(contasAReceber);

            var totParcelas = request.Parcelas.Count;
            for (int i = 0; i < totParcelas; i++)
            {
                var parcela = request.Parcelas[i];

                var novaConta = new ContasAReceberModel
                {
                    LojaId = contasAReceber.LojaId,
                    MovimentacaoId = contasAReceber.MovimentacaoId,
                    OriginalId = contasAReceber.Id,
                    NumParcela = i + 1,
                    TotParcela = totParcelas,
                    Valor = parcela.Valor,
                    MetodoPagamentoId = parcela.MetodoPagamentoId,
                    Status = StatusContaEnum.Pendente,
                    DataVencimento = parcela.DataVencimento,
                    DataPagamento = null,
                };

                await _context.ContasAReceber.AddAsync(novaConta, cancellationToken);
            }

            await _context.SaveChangesAsync();

            return contasAReceber;
        }
    }
}
