using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Data.Configurations
{
    public class PartnerContactInfoConfiguration : IEntityTypeConfiguration<PartnerContactInfo>
    {
        public void Configure(EntityTypeBuilder<PartnerContactInfo> builder)
        {
            builder.ToTable("PartnerContactInfo", "Subjects");
            builder.Property(m => m.Content)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasIndex(m => m.PartnerId)
                .HasName("IX_PartnerContactInfo_Partner");
            builder.HasIndex(m => m.ContactTypeId)
                .HasName("IX_PartnerContactInfo_ContactType");
            builder.HasIndex(m => new { m.PartnerId, m.ContactTypeId })
                .HasName("UQ_PartnerContactInfo_Partner_ContactType")
                .IsUnique();
            builder.HasOne(m => m.Partner)
                .WithMany(m => m.PartnerContactInfos)
                .HasForeignKey(m => m.PartnerId);
            builder.HasOne(m => m.ContactType)
                .WithMany(m => m.PartnerContactTypes)
                .HasForeignKey(m => m.ContactTypeId);
        }
    }
}
