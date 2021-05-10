using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoEletronico.API.Models
{
    public class ItemPedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idItem { get; set; }

        [ForeignKey("Pedido")]
        public int idPedido { get; set; }

        public string descricao { get; set; }

        public decimal precoUnitario { get; set; }

        public int qtd { get; set; }

        public Pedido Pedido { get; set; }
    }
}
