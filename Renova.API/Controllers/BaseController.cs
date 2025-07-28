using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
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
    }
}
