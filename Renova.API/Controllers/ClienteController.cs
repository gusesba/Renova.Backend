using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Commands.Cliente;
using Renova.Service.Queries.Cliente;

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
                await IsLojaFromUser(command);

                if (command.UsuarioId == null && (command.Email == null || command.Nome == null))
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
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro Inesperado. Mensagem: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClienteById([FromRoute] Guid id, [FromQuery] GetClienteByIdQuery query)
        {
            try
            {
                await IsLojaFromUser(query);

                query.Id = id;
                var cliente = await _mediator.Send(query);

                return Ok(cliente);
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> AtualizarCliente([FromRoute] Guid id, [FromBody] EditarClienteCommand command)
        {
            try
            {
                await IsLojaFromUser(command);

                command.Id = id;
                var cliente = await _mediator.Send(command);

                return Ok(cliente);
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

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<ClienteModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClientesFromLojaId([FromQuery] GetClientesFromLojaIdQuery query)
        {
            try
            {
                await IsLojaFromUser(query);

                var clientes = await _mediator.Send(query);

                return Ok(clientes);
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

        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClienteByEmail([FromRoute] string email, [FromQuery] GetClienteByEmailQuery query)
        {
            try
            {
                await IsLojaFromUser(query);
                query.Email = email;

                var cliente = await _mediator.Send(query);

                return Ok(cliente);
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
