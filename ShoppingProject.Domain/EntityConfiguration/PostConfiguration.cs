using ShoppingProject.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingProject.Domain.EntityConfiguration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(a => a.PostId);
            builder.Property(a => a.PostId).ValueGeneratedOnAdd();
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
            builder.Property(a => a.AuthorId)
                .IsRequired(false);
            builder.Property(a => a.CategoryId)
                .IsRequired(true);
            builder.Property(a => a.CoverUrl)
                .IsRequired(false);
            builder.HasOne<ApplicationUser>(a => a.Author)
                .WithMany(b => b.Posts)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne<PostCategory>(a => a.Categories)
                .WithMany(b => b.Posts)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
