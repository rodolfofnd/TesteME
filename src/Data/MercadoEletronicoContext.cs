using MercadoEletronico.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoEletronico.API.Data
{
    public class MercadoEletronicoContext : DbContext, IUnitOfWork
    {
        public MercadoEletronicoContext(DbContextOptions<MercadoEletronicoContext> options)
            : base(options)
        { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Pedido
            builder.Entity<Pedido>()
                .ToTable("Pedido")
                .HasKey(table => table.idPedido);

            builder.Entity<Pedido>()
                .Property(e => e.idPedido)
                .ValueGeneratedOnAdd();

            // Grupos
            builder.Entity<ItemPedido>()
                .ToTable("ItemPedido")
                .HasKey(table => table.idItem);
            
            builder.Entity<ItemPedido>()
                .Property(e => e.idItem)
                .ValueGeneratedOnAdd();

                builder.Entity<ItemPedido>()
            .HasOne(p => p.Pedido)
            .WithMany(b => b.itens);

            builder.ApplyConfigurationsFromAssembly(typeof(MercadoEletronicoContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            return sucesso;

        }


    }

}
