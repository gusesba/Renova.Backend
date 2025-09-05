using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Commands.ContasAPagar;
using Renova.Service.Queries.ContasAPagar;

namespace Renova.API.Controllers
{

    [Route("api/contasAPagar")]
    public class ContasAPagarController : BaseController
    {
        public ContasAPagarController(IMediator mediator) : base(mediator) { }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ContasAPagarModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> PagarContaAPagar([FromRoute] Guid id, [FromBody] PagarContasAPagarCommand command)
        {
            try
            {
                await IsLojaFromUser(command);

                command.Id = id;
                var contaAPagar = await _mediator.Send(command);

                return Ok(contaAPagar);
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
        [ProducesResponseType(typeof(PagedResult<ContasAPagarModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContasAPagarByLojaId([FromQuery] GetContasAPagarFromLojaIdQuery command)
        {
            try
            {
                await IsLojaFromUser(command);

                var contasAPagar = await _mediator.Send(command);

                return Ok(contasAPagar);
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
