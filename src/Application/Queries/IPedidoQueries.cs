using System.Collections.Generic;
using System.Threading.Tasks;
using MercadoEletronico.API.Application.ViewModels;

namespace MercadoEletronico.API.Applications.Queries
{
    public interface IPedidoQueries
    {
        Task<PedidoViewModel> ObterPedido(string pedido);
        Task<StatusResponse> ObterStatus(StatusRequest statusRequest);
    }
}
