using MercadoEletronico.API.Core;
using MercadoEletronico.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoEletronico.API.Data.Repository
{
    public class ItemPedidoRepository : IItemPedidoRepository
    {

        private readonly MercadoEletronicoContext _context;

        public ItemPedidoRepository(MercadoEletronicoContext context)
        {
            _context = context;
        }
        
        public IUnitOfWork UnitOfWork => _context;

        public void CriarItemPedido(ItemPedido item)
        {
             _context.ItensPedido.Add(item); 
        }

        public void ExcluirItemPedido(ItemPedido item)
        {
             _context.ItensPedido.Remove(item); 
        }

        public async Task<List<ItemPedido>> ListarPorId(int idPedido)
        {
             return await _context.ItensPedido.Where(a => a.idPedido == idPedido).ToListAsync();
        }

        public DbConnection ObterConexao() => _context.Database.GetDbConnection();

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
