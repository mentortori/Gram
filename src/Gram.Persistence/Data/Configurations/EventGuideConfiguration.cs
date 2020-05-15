using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Data.Configurations
{
    public class EventGuideConfiguration : IEntityTypeConfiguration<EventGuide>
    {
        public void Configure(EntityTypeBuilder<EventGuide> builder)
        {
            builder.ToTable("EventGuide", "Events");
            builder.HasIndex(m => m.EventId)
                .HasName("IX_EventGuide_Event");
            builder.HasIndex(m => m.GuideId)
                .HasName("IX_EventGuide_Guide");
            builder.HasIndex(m => new { m.EventId, m.GuideId })
                .HasName("UQ_EventGuide_Event_Guide")
                .IsUnique();
            builder.HasOne(m => m.Event)
                .WithMany(m => m.EventGuides)
                .HasForeignKey(m => m.EventId);
            builder.HasOne(m => m.Guide)
                .WithMany(m => m.EventGuides)
                .HasForeignKey(m => m.GuideId);
        }
    }
}
