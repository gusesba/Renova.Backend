using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model;
using Renova.Service.Commands.Loja;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Renova.Service.Queries.Usuario;

namespace Renova.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/loja")]
    public class LojaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LojaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        [ProducesResponseType(typeof(LojaModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarLoja([FromBody] CriarLojaCommand command)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var usuario = await _mediator.Send(new GetUsuarioByEmailQuery() { Email = email });

            command.UsuarioId = usuario.Id;

            var loja = await _mediator.Send(command);

            return Ok(loja);
        }
    }
}
