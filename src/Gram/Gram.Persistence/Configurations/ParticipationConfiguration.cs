using Gram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Gram.Persistence.Configurations
{
    public class ParticipationConfiguration : IEntityTypeConfiguration<Participation>
    {
        public void Configure(EntityTypeBuilder<Participation> builder)
        {
            builder.ToTable("Participation", "Events");
            builder.Property(m => m.StatusDate)
                .HasColumnType("date")
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();
            builder.Property(m => m.Remarks)
                .HasMaxLength(50);
            builder.HasIndex(m => m.EventId)
                .HasName("IX_Participation_Event");
            builder.HasOne(m => m.Event)
                .WithMany(m => m.EventParticipations)
                .HasForeignKey(m => m.EventId);
            builder.HasIndex(m => m.PersonId)
                .HasName("IX_Participation_Person");
            builder.HasOne(m => m.Person)
                .WithMany(m => m.ParticipatingPeople)
                .HasForeignKey(m => m.PersonId);
            builder.HasIndex(m => m.StatusId)
                .HasName("IX_Participation_Status");
            builder.HasOne(m => m.Status)
                .WithMany(m => m.ParticipationStatus)
                .HasForeignKey(m => m.StatusId);
        }
    }
}
