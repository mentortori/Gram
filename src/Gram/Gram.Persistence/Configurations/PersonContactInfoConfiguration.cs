using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Configurations
{
    public class PersonContactInfoConfiguration : IEntityTypeConfiguration<PersonContactInfo>
    {
        public void Configure(EntityTypeBuilder<PersonContactInfo> builder)
        {
            builder.ToTable("PersonContactInfo", "Subjects");
            builder.Property(m => m.Content)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasIndex(m => m.PersonId)
                .HasName("IX_PersonContactInfo_Person");
            builder.HasIndex(m => m.ContactTypeId)
                .HasName("IX_PersonContactInfo_ContactType");
            builder.HasIndex(m => new { m.PersonId, m.ContactTypeId })
                .HasName("UQ_PersonContactInfo_Person_ContactType")
                .IsUnique();
            builder.HasOne(m => m.Person)
                .WithMany(m => m.PersonContactInfos)
                .HasForeignKey(m => m.PersonId);
            builder.HasOne(m => m.ContactType)
                .WithMany(m => m.ContactTypes)
                .HasForeignKey(m => m.ContactTypeId);
        }
    }
}
