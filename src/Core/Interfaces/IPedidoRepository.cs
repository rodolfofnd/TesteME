using System.Data.Common;
using System.Threading.Tasks;
using MercadoEletronico.API.Data;
using MercadoEletronico.API.Models;

namespace MercadoEletronico.API.Core
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Pedido CriarPedido(Pedido pedido);
        Task<Pedido> ObterPorPedido(string pedido);
        void ExcluirPedido(Pedido pedido);
        DbConnection ObterConexao();
    }
}
