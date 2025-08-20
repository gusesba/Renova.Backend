using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Domain.Model.Dto;
using Renova.Domain.Settings;
using Renova.Persistence;
using Renova.Service.Helpers;
using Renova.Service.Queries.Movimentacao;

namespace Renova.Service.Handlers.Movimentacao
{
    public class GetMovimentacaosFromLojaIdQueryHandler : IRequestHandler<GetMovimentacoesFromLojaIdQuery, PagedResult<MovimentacaoDto>>
    {
        private readonly RenovaDbContext _context;

        public GetMovimentacaosFromLojaIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<MovimentacaoDto>> Handle(GetMovimentacoesFromLojaIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Movimentacao
                .Include(m => m.ProdutoMovimentacoes)
                .Include(m => m.ContasAReceber)
                .Where(m => m.LojaId == request.LojaId);

            if (request.Tipo != null)
                query = query.Where(p => p.Tipo == request.Tipo);

            if (request.Data != null)
                query = FiltroHelper.ApplyDateFilter(query, request.Data, p => p.Data);

            if (request.StatusConta != null)
                query = query.Where(m => m.ContasAReceber.Any(c => c.Status == request.StatusConta));

            if (!string.IsNullOrWhiteSpace(request.TotalItens))
            {
                var filtro = request.TotalItens;
                if (filtro.Contains("|"))
                {
                    var parts = filtro.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    if (int.TryParse(parts[0], out var min) && int.TryParse(parts[1], out var max))
                        query = query.Where(m => m.ProdutoMovimentacoes.Count >= min && m.ProdutoMovimentacoes.Count <= max);
                }
                else if (filtro.StartsWith(">") && int.TryParse(filtro[1..], out var greater))
                    query = query.Where(m => m.ProdutoMovimentacoes.Count > greater);
                else if (filtro.StartsWith("<") && int.TryParse(filtro[1..], out var less))
                    query = query.Where(m => m.ProdutoMovimentacoes.Count < less);
                else if ((filtro.StartsWith("=") && int.TryParse(filtro[1..], out var equal)) ||
                         int.TryParse(filtro, out equal))
                    query = query.Where(m => m.ProdutoMovimentacoes.Count == equal);
            }

            if (!string.IsNullOrWhiteSpace(request.Valor))
            {
                if (request.Valor.Contains("|"))
                {
                    var parts = request.Valor.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    if (decimal.TryParse(parts[0], out var min) && decimal.TryParse(parts[1], out var max))
                        query = query.Where(m => m.ContasAReceber.Where(c => c.OriginalId == null).Any(c => c.Valor >= min && c.Valor <= max));
                }
                else if (request.Valor.StartsWith(">") && decimal.TryParse(request.Valor[1..], out var greater))
                    query = query.Where(m => m.ContasAReceber.Where(c => c.OriginalId == null).Any(c => c.Valor > greater));
                else if (request.Valor.StartsWith("<") && decimal.TryParse(request.Valor[1..], out var less))
                    query = query.Where(m => m.ContasAReceber.Where(c => c.OriginalId == null).Any(c => c.Valor < less));
                else if ((request.Valor.StartsWith("=") && decimal.TryParse(request.Valor[1..], out var equal)) ||
                         decimal.TryParse(request.Valor, out equal))
                    query = query.Where(m => m.ContasAReceber.Where(c => c.OriginalId == null).Any(c => c.Valor == equal));
            }

            bool ascending = request.OrderDirection?.ToLower() != "desc";
            query = request.OrderBy?.ToLower() switch
            {
                "tipo" => ascending ? query.OrderBy(m => m.Tipo) : query.OrderByDescending(m => m.Tipo),
                "data" => ascending ? query.OrderBy(m => m.Data) : query.OrderByDescending(m => m.Data),
                "totalitens" => ascending ? query.OrderBy(m => m.ProdutoMovimentacoes.Count) : query.OrderByDescending(m => m.ProdutoMovimentacoes.Count),
                "valor" => ascending ? query.OrderBy(m => m.ContasAReceber.Where(c => c.OriginalId == null).Sum(c => c.Valor))
                                     : query.OrderByDescending(m => m.ContasAReceber.Where(c => c.OriginalId == null).Sum(c => c.Valor)),
                _ => ascending ? query.OrderBy(m => m.Data) : query.OrderByDescending(m => m.Data)
            };

            var totalCount = await query.CountAsync(cancellationToken);
            var skip = (request.Page - 1) * request.PageSize;

            var movimentacoes = await query
                .Skip(skip)
                .Take(request.PageSize)
                .Select(m => new MovimentacaoDto
                {
                    Id = m.Id,
                    Tipo = m.Tipo,
                    Data = m.Data,
                    TotalItens = m.ProdutoMovimentacoes.Count,
                    ContasAReceber = m.ContasAReceber,
                    ProdutoMovimentacoes = m.ProdutoMovimentacoes,
                    Valor = m.ContasAReceber.Where(c => c.OriginalId == null).Sum(c => c.Valor)
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<MovimentacaoDto>
            {
                Items = movimentacoes,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                CurrentPage = request.Page
            };
        }

    }
}
