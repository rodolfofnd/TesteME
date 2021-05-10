using MercadoEletronico.API.Core;
using MercadoEletronico.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoEletronico.API.Data.Repository
{
    public class PedidoRepository : IPedidoRepository
    {

        private readonly MercadoEletronicoContext _context;

        public PedidoRepository(MercadoEletronicoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Pedido CriarPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            return pedido;
        }

        public async Task<Pedido> ObterPorPedido(string pedido)
        {
            return await _context.Pedidos.FirstOrDefaultAsync(c => c.pedido == pedido);
        }

        public void ExcluirPedido(Pedido pedido)
        {
            _context.Pedidos.Remove(pedido);
        }

        public DbConnection ObterConexao() => _context.Database.GetDbConnection();

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
