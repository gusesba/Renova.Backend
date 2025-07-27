using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model.Dto;
using Renova.Service.Commands.Auth;
using Renova.Service.Queries.Auth;

namespace Renova.API.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginQuery request)
        {
            var token = await _mediator.Send(request);
            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [HttpPost("registrar")]
        [ProducesResponseType(typeof(LoginDto), StatusCodes.Status201Created)]

        public async Task<IActionResult> Registrar([FromBody] SignUpCommand command)
        {
            var token = await _mediator.Send(command);

            return Ok(token);
        }
    }
}
