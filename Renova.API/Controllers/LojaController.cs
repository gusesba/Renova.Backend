using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model;
using Renova.Service.Commands.Loja;
using Renova.Service.Queries.Loja;

namespace Renova.API.Controllers
{

    [Route("api/loja")]
    public class LojaController : BaseController
    {
        public LojaController(IMediator mediator) : base(mediator) { }

        [HttpPost()]
        [ProducesResponseType(typeof(LojaModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarLoja([FromBody] CriarLojaCommand command)
        {
            try
            {
                var usuario = await GetUsuarioByToken();
                command.UsuarioId = usuario.Id;

                var loja = await _mediator.Send(command);

                return Created($"api/loja/{loja.Id}", loja);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro Inesperado. Mensagem: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLojaById([FromRoute] Guid id)
        {
            try
            {
                var usuario = await GetUsuarioByToken();

                var loja = await _mediator.Send(new GetLojaByIdQuery() { Id = id });

                if (loja?.UsuarioId != usuario.Id)
                    return Unauthorized();

                return Ok(loja);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro Inesperado. Mensagem: " + e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLojasFromUsuario()
        {
            try
            {
                var usuario = await GetUsuarioByToken();

                var lojas = await _mediator.Send(new GetLojasFromUsuarioIdQuery() { UsuarioId = usuario.Id });

                return Ok(lojas);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro Inesperado. Mensagem: " + e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoja([FromRoute] Guid id)
        {
            try
            {
                var usuario = await GetUsuarioByToken();
                await _mediator.Send(new DeleteLojaCommand() { Id = id, UsuarioId = usuario.Id });

                return StatusCode(204);
            }
            catch (UnauthorizedAccessException e)
            {
                return NotFound(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro Inesperado. Mensagem: " + e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarLoja([FromRoute] Guid id, [FromBody] EditarLojaCommand command)
        {
            try
            {
                var usuario = await GetUsuarioByToken();
                command.Id = id;
                command.UsuarioId = usuario.Id;

                var loja = await _mediator.Send(command);

                return Ok(loja);
            }
            catch (UnauthorizedAccessException e)
            {
                return NotFound(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro Inesperado. Mensagem: " + e.Message);
            }
        }
    }
}
