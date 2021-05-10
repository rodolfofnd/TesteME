using System.Collections.Generic;
using System.Linq;
using MercadoEletronico.API.Models;

namespace MercadoEletronico.API.Application.ViewModels
{
    public class StatusResponse
    {
        public string pedido { get; set; }
        public List<string> status { get; set; }
    }
}
