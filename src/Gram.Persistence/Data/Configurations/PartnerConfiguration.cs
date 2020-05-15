using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Data.Configurations
{
    public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.ToTable("Partner", "Subjects");
            builder.Property(m => m.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(m => m.Name)
                .HasName("UQ_Partner_Name")
                .IsUnique();
        }
    }
}
