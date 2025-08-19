using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Commands.PropriedadeProduto;


namespace Renova.Service.Handlers.PropriedadeProduto
{
    public class CriarTipoCommandHandler : IRequestHandler<CriarTipoCommand, TipoProdutoModel>
    {
        private readonly RenovaDbContext _context;
        public CriarTipoCommandHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<TipoProdutoModel> Handle(CriarTipoCommand request, CancellationToken cancellationToken)
        {
            var tipo = await _context.TipoProduto.Where(tipo => tipo.Valor.ToLower() == request.Valor.ToLower() && tipo.LojaId == request.LojaId).FirstOrDefaultAsync();

            if (tipo != null)
            {
                throw new ValidationException("A tipo já existe");
            }

            tipo = new()
            {
                Valor = request.Valor,
                LojaId = request.LojaId
            };

            var createdTipo = await _context.TipoProduto.AddAsync(tipo, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return createdTipo.Entity;
        }
    }
}
