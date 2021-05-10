using AutoMapper;
using Dapper;
using MercadoEletronico.API.Application.ViewModels;
using MercadoEletronico.API.Core;
using MercadoEletronico.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoEletronico.API.Applications.Queries
{
    public class PedidoQueries : IPedidoQueries
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoQueries(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoViewModel> ObterPedido(string pedido)
        {
            var pedidoDictionary = new Dictionary<int, Pedido>();

            const string sql = @"SELECT * 
                                FROM Pedido p 
                                LEFT JOIN ItemPedido ip on (p.idPedido = ip.idPedido) 
                                WHERE p.pedido = @pedido";

            var consulta = await _pedidoRepository.ObterConexao().QueryAsync<Pedido, ItemPedido, Pedido>
                (sql, (pedido, itemPedido) =>
                {
                    Pedido pedidoEntry;

                    if (!pedidoDictionary.TryGetValue(pedido.idPedido, out pedidoEntry))
                    {
                        pedidoEntry = pedido;
                        pedidoEntry.itens = new List<ItemPedido>();
                        pedidoDictionary.Add(pedidoEntry.idPedido, pedidoEntry);
                    }

                    pedidoEntry.itens.Add(itemPedido);

                    return pedido;
                },
            splitOn: "idItem",
            param: new { pedido });

            return (PedidoViewModel)consulta.AsList<Pedido>().First();
        }

        public async Task<StatusResponse> ObterStatus(StatusRequest statusRequest)
        {
            var pedido = await _pedidoRepository.ObterPorPedido(statusRequest.pedido);

            const string sql = @"SELECT sum(qtd) as qtdTotal, 
                                sum(precoUnitario) as valorTotal 
                                from ItemPedido where idPedido = @idPedido";

            var result = await _pedidoRepository.ObterConexao().QueryAsync<dynamic>
                (sql, param: new { pedido.idPedido });


            if (result.AsList().Count == 0)
                throw new KeyNotFoundException();

            var StatusResponse = MapearRetorno(result, statusRequest);
            StatusResponse.pedido = pedido.pedido;

            return StatusResponse;
        }

        private StatusResponse MapearRetorno(dynamic result, StatusRequest statusRequest)
        {
            var statusResponse = new StatusResponse();
            statusResponse.status = new List<string>();

            var qtdTotal = (int)result[0].qtdTotal;
            var valorTotal = (decimal)result[0].valorTotal;

            if (statusRequest.status == "REPROVADO")
            {
                statusResponse.status.Add("REPROVADO");
                return statusResponse;
            }

            if (statusRequest.status == "APROVADO")
            {
                if (statusRequest.itensAprovados == qtdTotal && statusRequest.valorAprovado == valorTotal)
                    statusResponse.status.Add("APROVADO");

                if (statusRequest.valorAprovado < valorTotal)
                    statusResponse.status.Add("APROVADO_VALOR_A_MENOR");

                if (statusRequest.itensAprovados < qtdTotal)
                    statusResponse.status.Add("APROVADO_QTD_A_MENOR");

                if (statusRequest.valorAprovado > valorTotal)
                    statusResponse.status.Add("APROVADO_VALOR_A_MAIOR");

                if (statusRequest.itensAprovados > qtdTotal)
                    statusResponse.status.Add("APROVADO_QTD_A_MAIOR");
            }

            return statusResponse;
        }
    }
}
