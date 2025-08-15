using Renova.Domain.Model;
using Renova.Persistence;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Renova.Service.Commands.PropriedadeProduto;
using Microsoft.EntityFrameworkCore;


namespace Renova.Service.Handlers.PropriedadeProduto
{
    public class CriarMarcaCommandHandler : IRequestHandler<CriarMarcaCommand, MarcaProdutoModel>
    {
        private readonly RenovaDbContext _context;
        public CriarMarcaCommandHandler(RenovaDbContext context)
        {
            _context = context;
        }

        public async Task<MarcaProdutoModel> Handle(CriarMarcaCommand request, CancellationToken cancellationToken)
        {
            var marca = await _context.MarcaProduto.Where(marca => marca.Valor.ToLower() == request.Valor.ToLower() && marca.LojaId == request.LojaId).FirstOrDefaultAsync();

            if(marca != null)
            {
                throw new ValidationException("A marca já existe");
            }

            marca = new()
            {
                Valor = request.Valor,
                LojaId = request.LojaId
            };

            var createdMarca = await _context.MarcaProduto.AddAsync(marca, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return createdMarca.Entity;
        }
    }
}
