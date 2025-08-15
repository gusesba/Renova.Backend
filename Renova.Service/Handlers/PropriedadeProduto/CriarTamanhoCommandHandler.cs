using Renova.Domain.Model;
using Renova.Persistence;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Renova.Service.Commands.PropriedadeProduto;
using Microsoft.EntityFrameworkCore;


namespace Renova.Service.Handlers.PropriedadeProduto
{
    public class CriarTamanhoCommandHandler : IRequestHandler<CriarTamanhoCommand, TamanhoProdutoModel>
    {
        private readonly RenovaDbContext _context;
        public CriarTamanhoCommandHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<TamanhoProdutoModel> Handle(CriarTamanhoCommand request, CancellationToken cancellationToken)
        {
            var tamanho = await _context.TamanhoProduto.Where(tamanho => tamanho.Valor.ToLower() == request.Valor.ToLower() && tamanho.LojaId == request.LojaId).FirstOrDefaultAsync();

            if(tamanho != null)
            {
                throw new ValidationException("A tamanho já existe");
            }

            tamanho = new()
            {
                Valor = request.Valor,
                LojaId = request.LojaId
            };

            var createdTamanho = await _context.TamanhoProduto.AddAsync(tamanho, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return createdTamanho.Entity;
        }
    }
}
