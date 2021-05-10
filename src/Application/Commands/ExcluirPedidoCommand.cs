using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using MercadoEletronico.API.Application.ViewModels;
using MercadoEletronico.API.Models;

namespace MercadoEletronico.API.Applications.Commands
{
    public class ExcluirPedidoCommand : IRequest<bool>
    {
        public string pedido { get; private set; }

        public ExcluirPedidoCommand(string pedido)
        {
            this.pedido = pedido;
        }

        public bool EhValido()
        {
            var validation = new ExcluirPedidoValidation().Validate(this);
            return validation.IsValid;
        }

        public class ExcluirPedidoValidation : AbstractValidator<ExcluirPedidoCommand>
        {
            public ExcluirPedidoValidation()
            {
                RuleFor(c => c.pedido)
                    .NotNull()
                    .WithMessage("Pedido Não encontrado");
            }

        }
    }



}
