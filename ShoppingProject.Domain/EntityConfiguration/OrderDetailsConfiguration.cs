using ShoppingProject.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingProject.Domain.EntityConfiguration
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(a => a.OrderDetailsId);
            builder.Property(a => a.OrderDetailsId).ValueGeneratedOnAdd();

            builder.Property(a => a.Price)
                .IsRequired(true);
            builder.Property(a => a.Quantity)
                .IsRequired(true);
            builder.Property(a => a.ProductId)
                .IsRequired(false);
            builder.Property(a => a.OrderId)
                .IsRequired(true);

            builder.HasOne<Order>(a => a.Order)
                .WithMany(b => b.OrderDetails)
                .HasForeignKey(c => c.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Product>(a => a.Product)
                .WithMany(b => b.OrderDetails)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
