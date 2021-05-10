namespace MercadoEletronico.API.Application.ViewModels
{
    public class StatusRequest
    {
        public string status { get; set; }
        public int itensAprovados { get; set; }
        public decimal valorAprovado { get; set; }
        public string pedido { get; set; }
    }
}
