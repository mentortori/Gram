using Gram.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Infrastructure.EntityConfigurations
{
    public class GeneralTypeConfiguration : IEntityTypeConfiguration<GeneralType>
    {
        public void Configure(EntityTypeBuilder<GeneralType> builder)
        {
            builder.ToTable("GeneralType", "General");
            builder.Property(m => m.Title)
                .HasMaxLength(60)
                .IsRequired();
            builder.Property(m => m.IsListed)
                .HasDefaultValue(true);
            builder.Property(m => m.IsFixed)
                .HasDefaultValue(false);
            builder.HasIndex(m => new { m.Title, m.ParentId })
                .HasName("UQ_GeneralType_Title_Parent")
                .HasFilter(null)
                .IsUnique();
            builder.HasIndex(m => m.ParentId)
                .HasName("IX_GeneralType_Parent");
            builder.HasOne(m => m.Parent)
                .WithMany(m => m.ChildTypes)
                .HasForeignKey(m => m.ParentId);
        }
    }
}
