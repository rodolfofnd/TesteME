using AutoMapper;
using MercadoEletronico.API.Application.ViewModels;
using MercadoEletronico.API.Models;

namespace MercadoEletronico.API.Application
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Pedido, PedidoViewModel>();
        }
    }
}
