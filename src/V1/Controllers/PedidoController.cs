using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using MercadoEletronico.API.Application.ViewModels;
using MercadoEletronico.API.Applications.Commands;
using MercadoEletronico.API.Applications.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MercadoEletronico.API.V1.Controllers
{
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/pedido")]
    [Route("api/pedido")]
    [ApiController]
    public class PedidoController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IPedidoQueries _pedidoQueries;

        public PedidoController(IMediator mediator,
        IPedidoQueries pedidoQueries)
        {
            _mediator = mediator;
            _pedidoQueries = pedidoQueries;
        }

        [HttpGet("{pedido}")]
        public async Task<IActionResult> Obter(string pedido)
        {
            try
            {
                if (String.IsNullOrEmpty(pedido)) return NotFound("PEDIDO_NÃO_ENCONTRADO");

                var resultado = await _pedidoQueries.ObterPedido(pedido);

                return resultado == null ? NotFound("PEDIDO_NÃO_ENCONTRADO") : Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{pedido}")]
        public async Task<IActionResult> Deletar(string pedido)
        {
            try
            {
                ExcluirPedidoCommand command = new ExcluirPedidoCommand(pedido);
                var response = await _mediator.Send(command);

                if (!response)
                    return BadRequest("ERRO_VALIDAÇÃO");

                return Ok("SUCESSO_EXCLUSÃO");
            }
            catch (Exception ex)
            {
                if (ex.Message == "PEDIDO_NÃO_ENCONTRADO")
                    return NotFound("PEDIDO_NÃO_ENCONTRADO");

                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Alterar([FromBody] AlterarPedidoCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                if (!response)
                    return BadRequest("ERRO_VALIDAÇÃO");

                return Ok("SUCESSO_ALTERAÇÃO");
            }
            catch (Exception ex)
            {
                if (ex.Message == "PEDIDO_NÃO_ENCONTRADO")
                    return NotFound("PEDIDO_NÃO_ENCONTRADO");

                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Incluir([FromBody] CriarPedidoCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok("SUCESSO_INCLUSÃO");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
