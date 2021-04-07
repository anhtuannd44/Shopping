using ShoppingProject.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingProject.Domain.EntityConfiguration
{
    public class WardConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> builder)
        {
            builder.ToTable("Ward");
            builder.HasKey(a => a.WardId);
            builder.Property(a => a.WardId).ValueGeneratedOnAdd();
            builder.Property(a => a.Name)
                .IsRequired(true);
            builder.HasOne<District>(a => a.Districts)
                .WithMany(b => b.Wards)
                .HasForeignKey(c => c.DistrictId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
