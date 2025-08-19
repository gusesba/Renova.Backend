using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model;
using Renova.Service.Config;
using Renova.Service.Queries.Loja;
using Renova.Service.Queries.Usuario;

namespace Renova.API.Controllers
{
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<UsuarioModel?> GetUsuarioByToken()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var usuario = await _mediator.Send(new GetUsuarioByEmailQuery() { Email = email });

            return usuario;
        }

        protected async Task IsLojaFromUser<TResponse>(BaseRequest<TResponse> request)
        {
            var loja = await _mediator.Send(new GetLojaByIdQuery() { Id = request.LojaId });
            var usuario = await GetUsuarioByToken();

            if (loja == null || usuario == null || loja.UsuarioId != usuario.Id)
                throw new UnauthorizedAccessException();
        }
    }
}
