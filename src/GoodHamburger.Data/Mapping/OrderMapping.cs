using GoodHamburger.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(O => O.Id);

        builder.Property(O => O.TotalOrder);

        builder.ToTable("Orders");
    }
}
