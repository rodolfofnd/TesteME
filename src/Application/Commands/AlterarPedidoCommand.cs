using System.Collections.Generic;
using FluentValidation;
using MediatR;
using MercadoEletronico.API.Application.ViewModels;

namespace MercadoEletronico.API.Applications.Commands
{
    public class AlterarPedidoCommand : IRequest<bool>
    {
        public string pedido { get; set; }
        public List<ItemPedidoViewModel> itens { get; set; }


        public AlterarPedidoCommand(string pedido, List<ItemPedidoViewModel> itens)
        {
            this.pedido = pedido;
            this.itens = itens;
        }

        public bool EhValido()
        {
            var validation = new AlterarPedidoValidation().Validate(this);
            return validation.IsValid;
        }

        public class AlterarPedidoValidation : AbstractValidator<AlterarPedidoCommand>
        {
            public AlterarPedidoValidation()
            {
                RuleFor(c => c.pedido)
                    .NotNull()
                    .WithMessage("Pedido Não encontrado");
            }

        }
    }



}
