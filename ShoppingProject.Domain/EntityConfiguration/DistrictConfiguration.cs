using ShoppingProject.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingProject.Domain.EntityConfiguration
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable("District");
            builder.HasKey(a => a.DistrictId);
            builder.Property(a => a.DistrictId).ValueGeneratedOnAdd();
            builder.Property(a => a.Name)
                .IsRequired(true);
            builder.HasOne<Province>(a => a.Provinces)
                .WithMany(b => b.Districts)
                .HasForeignKey(c => c.ProvinceId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
