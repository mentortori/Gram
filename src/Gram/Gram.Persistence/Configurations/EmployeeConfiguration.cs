using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee", "Subjects");
            builder.Property(m => m.UserId)
                .HasMaxLength(450)
                .IsRequired();
            builder.Property(m => m.DateOfEmployment)
                .HasColumnType("date");
            builder.Property(m => m.EmploymentExpiryDate)
                .HasColumnType("date");
            builder.HasIndex(m => m.PersonId)
                .HasName("IX_Employee_Person");
            builder.HasOne(m => m.Person)
                .WithMany(m => m.Employees)
                .HasForeignKey(m => m.PersonId);
        }
    }
}
