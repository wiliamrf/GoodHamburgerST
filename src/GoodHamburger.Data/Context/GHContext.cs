using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodHamburger.Business.Models;

namespace GoodHamburger.Data.Context
{
    public class GHContext : DbContext
    {
        public GHContext(DbContextOptions<GHContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasOne(p => p.Product) // Especifica a propriedade de navegação
                .WithMany() // Indica que o produto pode estar associado a muitos itens de pedido
                .HasForeignKey(p => p.ProductId); // Define a chave estrangeira
        }
    }
}
