using ShoppingProject.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingProject.Domain.EntityConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(a => a.OrderId);
            builder.Property(a => a.CustomerName)
                .IsRequired(true)
                .HasMaxLength(256);
            builder.Property(a => a.CustomerAddress)
                .IsRequired(true)
                .HasMaxLength(500);
            builder.Property(a => a.CustomerMessage)
                .IsRequired(false)
                .HasMaxLength(1000);
            builder.Property(a => a.CustomerPhoneNumber)
                .IsRequired(true)
                .HasMaxLength(50);
            builder.Property(a => a.CustomerEmail)
                .IsRequired(true)
                .HasMaxLength(50);
            builder.Property(a => a.CustomerId)
                .IsRequired(false);
            builder.HasOne<ApplicationUser>(a => a.Customer)
                .WithMany(b => b.Orders)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
