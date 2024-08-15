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
    public class ProductMapping :IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(P => P.Id);

            builder.Property(P => P.Name)
                   .IsRequired()
                    .HasColumnType("varchar(100)");

            builder.Property(P => P.Price)
                .IsRequired();

            builder.Property(P => P.productType)
                .IsRequired();

            builder.ToTable("Products");



        }
    }
}
