using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model;
using Renova.Service.Commands.Cliente;
using Renova.Service.Queries.Cliente;
using System.ComponentModel.DataAnnotations;

namespace Renova.API.Controllers
{

    [Route("api/cliente")]
    public class ClienteController : BaseController
    {
        public ClienteController(IMediator mediator) : base(mediator) { }

        [HttpPost()]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarCliente([FromBody] CriarClienteCommand command)
        {
            try
            {
                if (!(await IsLojaFromUser(command)))
                {
                    return Unauthorized();
                }

                if(command.UsuarioId == null && (command.Email == null || command.Nome == null))
                {
                    return BadRequest("Usuario Id ou Email/Nome devem estar preenchidos");
                }

                var cliente = await _mediator.Send(command);

                return Created($"api/cliente/{cliente.Id}", cliente);
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetClienteById([FromRoute] Guid id, [FromQuery] GetClienteByIdQuery query)
        {
            try
            {
                if (!(await IsLojaFromUser(query)))
                {
                    return Unauthorized();
                }

                query.Id = id;
                var cliente = await _mediator.Send(query);

                return Ok(cliente);
            }
            catch (Exception e) 
            {
                return StatusCode(500, "Erro Inesperado. Mensagem: " + e.Message);
            }

        }



    }
}
