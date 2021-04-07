using ShoppingProject.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingProject.Domain.EntityConfiguration
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable("Province");
            builder.HasKey(a => a.ProvinceId);
            builder.Property(a => a.ProvinceId).ValueGeneratedOnAdd();
            builder.Property(a => a.Name)
                .IsRequired(true);
        }
    }
}
