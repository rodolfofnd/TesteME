using MediatR;
using MercadoEletronico.API.Core;
using MercadoEletronico.API.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MercadoEletronico.API.Applications.Commands
{
    public class PedidoCommandHandler :
        IRequestHandler<CriarPedidoCommand, bool>,
        IRequestHandler<ExcluirPedidoCommand, bool>,
        IRequestHandler<AlterarPedidoCommand, bool>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IItemPedidoRepository _itemPedidoRepository;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository,
            IItemPedidoRepository itemPedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _itemPedidoRepository = itemPedidoRepository;
        }

        public async Task<bool> Handle(CriarPedidoCommand message, CancellationToken cancellationToken)
        {
            //validar se o comando é valido
            if (!message.EhValido()) return false;

            var pedido = _pedidoRepository.CriarPedido(new Pedido { pedido = message.pedido });
            await _pedidoRepository.UnitOfWork.Commit();

            foreach (var item in message.itens)
            {
                _itemPedidoRepository.CriarItemPedido(new ItemPedido
                {
                    descricao = item.descricao,
                    idPedido = pedido.idPedido,
                    precoUnitario = item.precoUnitario,
                    qtd = item.qtd
                });
            }

            return await _itemPedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AlterarPedidoCommand message, CancellationToken cancellationToken)
        {
            //validar se o comando é valido
            if (!message.EhValido()) return false;

            var pedido = await _pedidoRepository.ObterPorPedido(message.pedido);
            pedido.itens = await _itemPedidoRepository.ListarPorId(pedido.idPedido);

            foreach (var item in pedido.itens)
                _itemPedidoRepository.ExcluirItemPedido(item);

            await _itemPedidoRepository.UnitOfWork.Commit();

            foreach (var item in message.itens)
            {
                _itemPedidoRepository.CriarItemPedido(new ItemPedido
                {
                    descricao = item.descricao,
                    idPedido = pedido.idPedido,
                    precoUnitario = item.precoUnitario,
                    qtd = item.qtd
                });
            }

            return await _itemPedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ExcluirPedidoCommand message, CancellationToken cancellationToken)
        {
            //validar se o comando é valido
            if (!message.EhValido()) return false;

            var pedido = await _pedidoRepository.ObterPorPedido(message.pedido);
            pedido.itens = await _itemPedidoRepository.ListarPorId(pedido.idPedido);

            foreach (var item in pedido.itens)
                _itemPedidoRepository.ExcluirItemPedido(item);

            await _itemPedidoRepository.UnitOfWork.Commit();

            _pedidoRepository.ExcluirPedido(pedido);

            return await _pedidoRepository.UnitOfWork.Commit();
        }
    }
}
