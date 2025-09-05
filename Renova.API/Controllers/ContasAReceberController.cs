using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Commands.ContasAReceber;
using Renova.Service.Queries.ContasAReceber;

namespace Renova.API.Controllers
{

    [Route("api/contasAReceber")]
    public class ContasAReceberController : BaseController
    {
        public ContasAReceberController(IMediator mediator) : base(mediator) { }

        [HttpPut("pagar/{id}")]
        [ProducesResponseType(typeof(ContasAReceberModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> PagarContaAReceber([FromRoute] Guid id, [FromBody] PagarContasAReceberCommand command)
        {
            try
            {
                await IsLojaFromUser(command);

                command.Id = id;
                var contaAReceber = await _mediator.Send(command);

                return Ok(contaAReceber);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet()]
        [ProducesResponseType(typeof(PagedResult<ContasAReceberModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContasAReceberByLojaId([FromQuery] GetContasAReceberFromLojaIdQuery command)
        {
            try
            {
                await IsLojaFromUser(command);

                var contasAReceber = await _mediator.Send(command);

                return Ok(contasAReceber);
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
