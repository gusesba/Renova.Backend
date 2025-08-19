using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renova.Domain.Model;
using Renova.Domain.Settings;
using Renova.Service.Commands.Produto;
using Renova.Service.Queries.Produto;
using System.ComponentModel.DataAnnotations;

namespace Renova.API.Controllers
{

    [Route("api/produto")]
    public class ProdutoController : BaseController
    {
        public ProdutoController(IMediator mediator) : base(mediator) { }

        [HttpPost()]
        [ProducesResponseType(typeof(ProdutoModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarProduto([FromBody] CriarProdutoCommand command)
        {
            try
            {
                await IsLojaFromUser(command);

                var produto = await _mediator.Send(command);

                return Created($"api/produto/{produto.Id}", produto);
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
        [ProducesResponseType(typeof(PagedResult<ProdutoModel>), StatusCodes.Status201Created)]
        public async Task<IActionResult> GetProdutosByLojaId([FromQuery] GetProdutosFromLojaIdQuery command)
        {
            try
            {
                await IsLojaFromUser(command);

                var produtos = await _mediator.Send(command);

                return Ok(produtos);
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PagedResult<ProdutoModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AtualizarProduto([FromRoute] Guid id, [FromBody] EditarProdutoCommand command)
        {
            try
            {
                await IsLojaFromUser(command);

                command.Id = id;
                var produto = await _mediator.Send(command);

                return Ok(produto);
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
    }
}
