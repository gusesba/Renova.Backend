using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model.Dto;
using Renova.Domain.Settings;
using Renova.Persistence;
using Renova.Service.Helpers;
using Renova.Service.Queries.ContasAPagar;

namespace Renova.Service.Handlers.ContasAPagar
{
    public class GetContasAPagarFromLojaIdQueryHandler : IRequestHandler<GetContasAPagarFromLojaIdQuery, PagedResult<GetContasAPagarDto>>
    {
        private readonly RenovaDbContext _context;

        public GetContasAPagarFromLojaIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<GetContasAPagarDto>> Handle(GetContasAPagarFromLojaIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ContasAPagar
                .Include(c => c.MetodoPagamento)
                .Include(c => c.Movimentacao)
                .ThenInclude(c => c.Cliente)
                .Where(c => c.LojaId == request.LojaId);

            if (request.Valor != null)
            {
                query = FiltroHelper.ApplyDecimalFilter(query, request.Valor, p => p.Valor);
            }
            if (request.Status != null)
            {
                query = query.Where(q => q.Status == request.Status);
            }
            if (request.NumParcela != null)
            {
                query = FiltroHelper.ApplyDecimalFilter(query, request.NumParcela, p => p.NumParcela);
            }
            if (request.TotParcela != null)
            {
                query = FiltroHelper.ApplyDecimalFilter(query, request.TotParcela, p => p.TotParcela);
            }
            if (request.DataVencimento != null)
            {
                query = FiltroHelper.ApplyDateFilter(query, request.DataVencimento, p => p.DataVencimento);
            }
            if (request.DataPagamento != null)
            {
                query = FiltroHelper.ApplyDateFilter(query, request.DataPagamento, p => p.DataPagamento);
            }
            if (!string.IsNullOrWhiteSpace(request.MetodoPagamentoNome))
            {
                query = query.Where(p => p.MetodoPagamento.Nome.Contains(request.MetodoPagamentoNome));
            }
            if (request.MetodoPagamentoTaxa != null)
            {
                query = FiltroHelper.ApplyDecimalFilter(query, request.MetodoPagamentoTaxa, p => p.MetodoPagamento.Taxa);
            }
            if (request.ClienteId != null)
            {
                query = query.Where(p => p.Movimentacao.ClienteId == request.ClienteId);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            bool ascending = request.OrderDirection?.ToLower() != "desc";
            query = request.OrderBy?.ToLower() switch
            {
                "valor" => ascending ? query.OrderBy(c => c.Valor) : query.OrderByDescending(c => c.Valor),
                "status" => ascending ? query.OrderBy(c => c.Status) : query.OrderByDescending(c => c.Status),
                "numparcela" => ascending ? query.OrderBy(c => c.NumParcela) : query.OrderByDescending(c => c.NumParcela),
                "totparcela" => ascending ? query.OrderBy(c => c.TotParcela) : query.OrderByDescending(c => c.TotParcela),
                "taxa" => ascending ? query.OrderBy(c => c.MetodoPagamento.Taxa) : query.OrderByDescending(c => c.MetodoPagamento.Taxa),
                "nome" => ascending ? query.OrderBy(c => c.MetodoPagamento.Nome) : query.OrderByDescending(c => c.MetodoPagamento.Nome),
                "datapagamento" => ascending ? query.OrderBy(c => c.DataPagamento) : query.OrderByDescending(c => c.DataPagamento),
                "cliente" => ascending ? query.OrderBy(c => c.Movimentacao.Cliente.Apelido) : query.OrderByDescending(c => c.Movimentacao.Cliente.Apelido),
                "datavencimento" or _ => ascending ? query.OrderBy(c => c.DataVencimento) : query.OrderByDescending(c => c.DataVencimento),
            };

            var skip = (request.Page - 1) * request.PageSize;

            var contasAPagarModels = await query
                .Skip(skip)
                .Take(request.PageSize)
                .Select(m => new GetContasAPagarDto
                {
                    Id = m.Id,
                    Valor = m.Valor,
                    Status = m.Status,
                    OriginalId = m.OriginalId,
                    Original = m.Original,
                    NumParcela = m.NumParcela,
                    TotParcela = m.TotParcela,
                    DataVencimento = m.DataVencimento,
                    DataPagamento = m.DataPagamento,
                    MetodoPagamentoId = m.MetodoPagamentoId,
                    MetodoPagamento = m.MetodoPagamento,
                    MovimentacaoId = m.MovimentacaoId,
                    Movimentacao = new MovimentacaoClienteDto()
                    {
                        Id = m.Movimentacao.Id,
                        Cliente = m.Movimentacao.Cliente,
                        ClienteId = m.Movimentacao.ClienteId,
                        Data = m.Movimentacao.Data,
                        Tipo = m.Movimentacao.Tipo
                    },
                    LojaId = m.LojaId,
                    Loja = m.Loja,
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<GetContasAPagarDto>
            {
                Items = contasAPagarModels,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                CurrentPage = skip + 1
            };
        }
    }
}
