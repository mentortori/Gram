using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person", "Subjects");
            builder.Property(m => m.FirstName)
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(m => m.LastName)
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(m => m.DateOfBirth)
                .HasColumnType("date");
            builder.HasIndex(m => m.NationalityId)
                .HasName("IX_Person_Nationality");
            builder.HasOne(m => m.Nationality)
                .WithMany(m => m.PersonNationalities)
                .HasForeignKey(m => m.NationalityId);
        }
    }
}
