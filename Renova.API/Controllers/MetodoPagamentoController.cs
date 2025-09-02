using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Commands.MetodoPagamento;
using Renova.Service.Queries.MetodoPagamento;

namespace Renova.API.Controllers
{

    [Route("api/metodoPagamento")]
    public class MetodoPagamentoController : BaseController
    {
        public MetodoPagamentoController(IMediator mediator) : base(mediator) { }

        [HttpPost()]
        [ProducesResponseType(typeof(MetodoPagamentoModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarMetodoPagamento([FromBody] CriarMetodoPagamentoCommand command)
        {
            try
            {
                await IsLojaFromUser(command);

                var metodoPagamento = await _mediator.Send(command);

                return Created($"api/metodoPagamento/{metodoPagamento.Id}", metodoPagamento);
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

        [HttpGet()]
        [ProducesResponseType(typeof(PagedResult<MetodoPagamentoModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMetodosPagamentoByLojaId([FromQuery] GetMetodosPagamentoFromLojaIdQuery command)
        {
            try
            {
                await IsLojaFromUser(command);

                var metodosPagamento = await _mediator.Send(command);

                return Ok(metodosPagamento);
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
