using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event", "Events");
            builder.Property(m => m.EventName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(m => m.EventDescription)
                .HasMaxLength(4000)
                .IsRequired();
            builder.Property(m => m.EventDate)
                .HasColumnType("date");
            builder.HasIndex(m => m.EventStatusId)
                .HasName("IX_Event_EventStatus");
            builder.HasOne(m => m.EventStatus)
                .WithMany(m => m.EventStatuses)
                .HasForeignKey(m => m.EventStatusId);
        }
    }
}
