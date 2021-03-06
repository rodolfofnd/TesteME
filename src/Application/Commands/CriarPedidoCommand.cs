using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using MercadoEletronico.API.Application.ViewModels;
using MercadoEletronico.API.Models;

namespace MercadoEletronico.API.Applications.Commands
{
    public class CriarPedidoCommand : IRequest<bool>
    {
        public string pedido { get; set; }
        public List<ItemPedidoViewModel> itens { get; set; }


        public CriarPedidoCommand(string pedido, List<ItemPedidoViewModel> itens)
        {
            this.pedido = pedido;
            this.itens = itens;
        }

        public bool EhValido()
        {
            var validation = new CriarPedidoValidation().Validate(this);
            return validation.IsValid;
        }

        public class CriarPedidoValidation : AbstractValidator<CriarPedidoCommand>
        {
            public CriarPedidoValidation()
            {
                RuleFor(c => c.pedido)
                    .NotNull()
                    .WithMessage("Pedido Não encontrado");
            }

        }
    }



}
