using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Data.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Attendance", "Events");
            builder.Property(m => m.StatusDate)
                .HasColumnType("date")
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();
            builder.Property(m => m.Remarks)
                .HasMaxLength(50);
            builder.HasIndex(m => m.EventId)
                .HasName("IX_Attendance_Event");
            builder.HasIndex(m => m.PersonId)
                .HasName("IX_Attendance_Person");
            builder.HasIndex(m => m.StatusId)
                .HasName("IX_Attendance_Status");
            builder.HasIndex(m => new { m.EventId, m.PersonId })
                .HasName("UQ_Attendance_Event_Person")
                .IsUnique();
            builder.HasOne(m => m.Event)
                .WithMany(m => m.Attendees)
                .HasForeignKey(m => m.EventId);
            builder.HasOne(m => m.Person)
                .WithMany(m => m.Attendees)
                .HasForeignKey(m => m.PersonId);
            builder.HasOne(m => m.Status)
                .WithMany(m => m.AttendanceStatuses)
                .HasForeignKey(m => m.StatusId);
        }
    }
}
