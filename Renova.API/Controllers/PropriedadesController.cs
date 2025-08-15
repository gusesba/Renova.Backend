using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model;
using Renova.Service.Commands.PropriedadeProduto;
using System.ComponentModel.DataAnnotations;

namespace Renova.API.Controllers
{

    [Route("api/propriedades")]
    public class PropriedadesController : BaseController
    {
        public PropriedadesController(IMediator mediator) : base(mediator) { }

        [HttpPost("cor")]
        [ProducesResponseType(typeof(CorProdutoModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarCor([FromBody] CriarCorCommand command)
        {
            try
            {
                await IsLojaFromUser(command);

                var cor = await _mediator.Send(command);

                return Created($"api/propriedades/cor/{cor.Id}", cor);
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

        [HttpPost("tipo")]
        [ProducesResponseType(typeof(TipoProdutoModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarTipo([FromBody] CriarTipoCommand command)
        {
            try
            {
                await IsLojaFromUser(command);

                var tipo = await _mediator.Send(command);

                return Created($"api/propriedades/tipo/{tipo.Id}", tipo);
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

        [HttpPost("tamanho")]
        [ProducesResponseType(typeof(TamanhoProdutoModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarTamanho([FromBody] CriarTamanhoCommand command)
        {
            try
            {
                await IsLojaFromUser(command);

                var tamanho = await _mediator.Send(command);

                return Created($"api/propriedades/tamanho/{tamanho.Id}", tamanho);
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

        [HttpPost("marca")]
        [ProducesResponseType(typeof(MarcaProdutoModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarMarca([FromBody] CriarMarcaCommand command)
        {
            try
            {
                await IsLojaFromUser(command);

                var marca = await _mediator.Send(command);

                return Created($"api/propriedades/marca/{marca.Id}", marca);
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
