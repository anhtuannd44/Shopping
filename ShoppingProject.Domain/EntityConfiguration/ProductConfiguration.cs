using ShoppingProject.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingProject.Domain.EntityConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(a => a.ProductId);
            builder.Property(a => a.ProductId).ValueGeneratedOnAdd();
            builder.HasIndex(p => p.Slug)
                .IsUnique(true);
            builder.Property(a => a.Title)
                .IsRequired(true)
                .HasMaxLength(1000);
            builder.Property(a => a.Status)
                .IsRequired(true);
            builder.Property(a => a.DateCreated)
                .IsRequired(true);
            builder.Property(a => a.DateUpdated)
                .IsRequired(false);
            builder.Property(a => a.CoverUrl)
                .IsRequired(false);
        }
    }
}
