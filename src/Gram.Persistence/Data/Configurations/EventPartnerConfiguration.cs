using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Data.Configurations
{
    public class EventPartnerConfiguration : IEntityTypeConfiguration<EventPartner>
    {
        public void Configure(EntityTypeBuilder<EventPartner> builder)
        {
            builder.ToTable("EventPartner", "Events");
            builder.HasIndex(m => m.EventId)
                .HasName("IX_EventPartner_Event");
            builder.HasIndex(m => m.PartnerId)
                .HasName("IX_EventPartner_Partner");
            builder.HasIndex(m => new { m.EventId, m.PartnerId })
                .HasName("UQ_EventPartner_Event_Partner")
                .IsUnique();
            builder.HasOne(m => m.Event)
                .WithMany(m => m.EventPartners)
                .HasForeignKey(m => m.EventId);
            builder.HasOne(m => m.Partner)
                .WithMany(m => m.EventPartners)
                .HasForeignKey(m => m.PartnerId);
        }
    }
}
