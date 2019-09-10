using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Configurations
{
    public class GuideConfiguration : IEntityTypeConfiguration<Guide>
    {
        public void Configure(EntityTypeBuilder<Guide> builder)
        {
            builder.ToTable("Guide", "Subjects");
            builder.HasIndex(m => m.PersonId)
                .HasName("IX_Guide_Person");
            builder.HasIndex(m => m.PersonId )
                .HasName("UQ_Guide_Person")
                .IsUnique();
            builder.HasOne(m => m.Person)
                .WithMany(m => m.Guides)
                .HasForeignKey(m => m.PersonId);
        }
    }
}
