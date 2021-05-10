using System.Threading.Tasks;
using MercadoEletronico.API.Application.ViewModels;
using MercadoEletronico.API.Applications.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MercadoEletronico.API.V1.Controllers
{
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/pedido")]
    [Route("api/status")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IPedidoQueries _pedidoQueries;

        public StatusController(IPedidoQueries pedidoQueries)
        {
            _pedidoQueries = pedidoQueries;
        }
       
        [HttpPost()]
        public async Task<IActionResult> Status([FromBody] StatusRequest statusRequest)
        {
            var resultado = await _pedidoQueries.ObterStatus(statusRequest);
            return Ok(resultado);
        }
    }
}
