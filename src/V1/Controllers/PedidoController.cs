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
            if (String.IsNullOrEmpty(pedido)) return NotFound();

            var resultado = await _pedidoQueries.ObterPedido(pedido);

            return resultado == null ? NotFound() : Ok(resultado);
        }

        [HttpDelete("{pedido}")]
        public async Task<IActionResult> Deletar(string pedido)
        {
            ExcluirPedidoCommand command = new ExcluirPedidoCommand(pedido);
            var response = await _mediator.Send(command);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Alterar([FromBody] AlterarPedidoCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok();
        }

        [HttpPost()]
        public async Task<IActionResult> Incluir([FromBody] CriarPedidoCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok();
        }

    }
}
