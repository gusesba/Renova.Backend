using Renova.Domain.Model;
using Renova.Domain.Model.Dto;
using Renova.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renova.Service.Commands.Auth;
using Renova.Service.Queries.Auth;
using System.ComponentModel.DataAnnotations;

namespace Renova.Service.Handlers.Auth
{
    public class CriarLojaCommand : IRequestHandler<SignUpCommand, LoginDto>
    {
        private readonly RenovaDbContext _context;
        private readonly IMediator _mediator;
        public CriarLojaCommand(RenovaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<LoginDto> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var usuarioExistente = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (usuarioExistente != null)
                throw new ValidationException("Email já cadastrado.");

            string senhaHash = "";
            if(request.Senha != "")
            {
                senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);
            }

            var novoUsuario = new UsuarioModel()
            {
                Email = request.Email,
                SenhaHash = senhaHash,
                Nome = request.Nome
            };

            await _context.Usuario.AddAsync(novoUsuario,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.Senha == "")
                return null;

            return await _mediator.Send(new LoginQuery()
            {
                Email = request.Email,
                Senha = request.Senha
            });
        }
    }
}
