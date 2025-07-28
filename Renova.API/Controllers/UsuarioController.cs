using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model.Dto;
using Renova.Service.Commands.Auth;
using Renova.Service.Queries.Auth;
using System.ComponentModel.DataAnnotations;

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
            try
            {
                var token = await _mediator.Send(request);
                if (token == null)
                    return Unauthorized();

                return Ok(token);
            } 
            catch (Exception e)
            {
                return StatusCode(500, "Erro Inesperado. Mensagem: " + e.Message);
            }
        }

        [HttpPost("registrar")]
        [ProducesResponseType(typeof(LoginDto), StatusCodes.Status201Created)]

        public async Task<IActionResult> Registrar([FromBody] SignUpCommand command)
        {
            try
            {
                var token = await _mediator.Send(command);
                return Ok(token);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro Inesperado. Mensagem: " + e.Message);
            }
        }
    }
}
