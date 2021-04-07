using ShoppingProject.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingProject.Domain.EntityConfiguration
{
    public class PostCategoryConfiguration : IEntityTypeConfiguration<PostCategory>
    {
        public void Configure(EntityTypeBuilder<PostCategory> builder)
        {
            builder.ToTable("PostCategory");
            builder.HasKey(a => a.CategoryId);
            builder.Property(a => a.CategoryId).ValueGeneratedOnAdd();
            builder.HasIndex(p => p.Slug)
                .IsUnique(true);
            builder.Property(a => a.Title)
                .IsRequired(true)
                .HasMaxLength(1000);
            builder.Property(a => a.Status)
                .IsRequired(true);
            builder.Property(a => a.CoverUrl)
                .IsRequired(false);
        }
    }
}
