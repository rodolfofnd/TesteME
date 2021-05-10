using System.Collections.Generic;
using MercadoEletronico.API.Models;

namespace MercadoEletronico.API.Application.ViewModels
{
    public class ItemPedidoViewModel
    {
        public string descricao { get; set; }

        public decimal precoUnitario { get; set; }

        public int qtd { get; set; }

        public static explicit operator ItemPedidoViewModel(ItemPedido model)
        {
            if (model == null)
                return null;

            var result = new ItemPedidoViewModel();

            result.descricao = model.descricao;
            result.precoUnitario = model.precoUnitario;
            result.qtd = model.qtd;
            
            return result;
        }

    }
}
