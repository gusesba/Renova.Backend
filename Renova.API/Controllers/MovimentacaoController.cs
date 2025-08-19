using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Commands.Movimentacao;

namespace Renova.API.Controllers
{

    [Route("api/movimentacao")]
    public class MovimentacaoController : BaseController
    {
        public MovimentacaoController(IMediator mediator) : base(mediator) { }

        [HttpPost()]
        [ProducesResponseType(typeof(MovimentacaoModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarMovimentacao([FromBody] CriarMovimentacaoCommand command)
        {
            try
            {
                await IsLojaFromUser(command);

                var movimentacao = await _mediator.Send(command);

                return Created($"api/movimentacao/{movimentacao.Id}", movimentacao);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro Inesperado. Mensagem: " + e.Message);
            }
        }
    }
}
