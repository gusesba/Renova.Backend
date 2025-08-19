using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Commands.PropriedadeProduto;


namespace Renova.Service.Handlers.PropriedadeProduto
{
    public class CriarCorCommandHandler : IRequestHandler<CriarCorCommand, CorProdutoModel>
    {
        private readonly RenovaDbContext _context;
        public CriarCorCommandHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<CorProdutoModel> Handle(CriarCorCommand request, CancellationToken cancellationToken)
        {
            var cor = await _context.CorProduto.Where(cor => cor.Valor.ToLower() == request.Valor.ToLower() && cor.LojaId == request.LojaId).FirstOrDefaultAsync();

            if (cor != null)
            {
                throw new ValidationException("A cor já existe");
            }

            cor = new()
            {
                Valor = request.Valor,
                LojaId = request.LojaId
            };

            var createdCor = await _context.CorProduto.AddAsync(cor, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return createdCor.Entity;
        }
    }
}
