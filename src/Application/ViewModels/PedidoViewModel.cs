using System.Collections.Generic;
using System.Linq;
using MercadoEletronico.API.Models;

namespace MercadoEletronico.API.Application.ViewModels
{
    public class PedidoViewModel
    {
        public string pedido { get; set; }

        public virtual ICollection<ItemPedidoViewModel> itens { get; set; }

        public static explicit operator PedidoViewModel(Pedido model)
        {
            if (model == null)
                return null;

            var result = new PedidoViewModel();

            result.pedido = model.pedido;
            result.itens = model.itens.Select(t => (ItemPedidoViewModel)t).ToList();

            return result;
        }
    }
}
