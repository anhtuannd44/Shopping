using ShoppingProject.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingProject.Domain.EntityConfiguration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question");
            builder.HasKey(a => a.QuestionId);
            builder.Property(a => a.QuestionId).ValueGeneratedOnAdd();
            builder.HasIndex(p => p.Title)
                .IsUnique(true);
            builder.Property(a => a.Reply)
                .IsRequired(true);
            builder.Property(a => a.Status)
                .IsRequired(true);
        }
    }
}
