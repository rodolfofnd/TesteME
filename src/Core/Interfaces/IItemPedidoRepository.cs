using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MercadoEletronico.API.Data;
using MercadoEletronico.API.Models;

namespace MercadoEletronico.API.Core
{
    public interface IItemPedidoRepository : IRepository<ItemPedido>
    {
        void CriarItemPedido(ItemPedido item);
        void ExcluirItemPedido(ItemPedido item);
        Task<List<ItemPedido>> ListarPorId(int idPedido);

        DbConnection ObterConexao();
    }
}
