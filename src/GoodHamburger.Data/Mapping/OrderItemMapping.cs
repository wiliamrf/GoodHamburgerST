using GoodHamburger.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Data.Mapping
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(OI => OI.Id);            

            builder.Property(OI => OI.ProductId)
                   .IsRequired();

            builder.Property(OI => OI.Quantity)
                   .IsRequired();
            builder.Property(OI => OI.productType)
                 .IsRequired();

            builder.ToTable("OrdemItems");


        }
    } 
}
