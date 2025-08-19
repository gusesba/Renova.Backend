using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Persistence;
using Renova.Service.Helpers;
using Renova.Service.Queries.Produto;

namespace Renova.Service.Handlers.Produto
{
    public class GetProdutosFromLojaIdQueryHandler : IRequestHandler<GetProdutosFromLojaIdQuery, PagedResult<ProdutoModel>>
    {
        private readonly RenovaDbContext _context;

        public GetProdutosFromLojaIdQueryHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ProdutoModel>> Handle(GetProdutosFromLojaIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Produto
                .Include(p => p.Fornecedor)
                .Include(p => p.Cor)
                .Include(p => p.Tamanho)
                .Include(p => p.Marca)
                .Include(p => p.Tipo)
                .Where(produto => produto.LojaId == request.LojaId);

            if (request.Referencia != null)
            {
                query = query.Where(p => p.Referencia == request.Referencia);
            }
            if (request.FornecedorId != null)
            {
                query = query.Where(p => p.FornecedorId == request.FornecedorId);
            }
            if (request.Preco != null)
            {
                query = FiltroHelper.ApplyDecimalFilter(query, request.Preco, p => p.Preco);
            }
            if (request.Status != null)
            {
                query = query.Where(p => p.Status == request.Status);
            }
            if (request.CorId != null)
            {
                query = query.Where(p => p.CorId == request.CorId);
            }
            if (request.TamanhoId != null)
            {
                query = query.Where(p => p.TamanhoId == request.TamanhoId);
            }
            if (request.MarcaId != null)
            {
                query = query.Where(p => p.MarcaId == request.MarcaId);
            }
            if (request.TipoId != null)
            {
                query = query.Where(p => p.TipoId == request.TipoId);
            }
            if (!string.IsNullOrWhiteSpace(request.Descricao))
            {
                query = query.Where(p => p.Descricao.Contains(request.Descricao));
            }
            if (request.DataEntrada != null)
            {
                query = FiltroHelper.ApplyDateFilter(query, request.DataEntrada, p => p.DataEntrada);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            bool ascending = request.OrderDirection?.ToLower() != "desc";
            query = request.OrderBy?.ToLower() switch
            {
                "fornecedor" => ascending ? query.OrderBy(c => c.Fornecedor.Apelido) : query.OrderByDescending(c => c.Fornecedor.Apelido),
                "preco" => ascending ? query.OrderBy(c => c.Preco) : query.OrderByDescending(c => c.Preco),
                "status" => ascending ? query.OrderBy(c => c.Status) : query.OrderByDescending(c => c.Status),
                "cor" => ascending ? query.OrderBy(c => c.Cor.Valor) : query.OrderByDescending(c => c.Cor.Valor),
                "tamanho" => ascending ? query.OrderBy(c => c.Tamanho.Valor) : query.OrderByDescending(c => c.Tamanho.Valor),
                "marca" => ascending ? query.OrderBy(c => c.Marca.Valor) : query.OrderByDescending(c => c.Marca.Valor),
                "tipo" => ascending ? query.OrderBy(c => c.Tipo.Valor) : query.OrderByDescending(c => c.Tipo.Valor),
                "descricao" => ascending ? query.OrderBy(c => c.Descricao) : query.OrderByDescending(c => c.Descricao),
                "dataentrada" => ascending ? query.OrderBy(c => c.DataEntrada) : query.OrderByDescending(c => c.DataEntrada),
                "referencia" or _ => ascending ? query.OrderBy(c => c.Referencia) : query.OrderByDescending(c => c.Referencia),
            };

            var skip = (request.Page - 1) * request.PageSize;

            var produtos = await query
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<ProdutoModel>
            {
                Items = produtos,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                CurrentPage = skip + 1
            };
        }
    }
}
