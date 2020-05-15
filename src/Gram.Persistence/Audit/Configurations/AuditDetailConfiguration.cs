using Gram.Persistence.Audit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Audit.Configurations
{
    public class AuditDetailConfiguration : IEntityTypeConfiguration<AuditDetail>
    {
        public void Configure(EntityTypeBuilder<AuditDetail> builder)
        {
            builder.ToTable("AuditDetail", "Audit");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Property)
                .HasMaxLength(128)
                .IsRequired();
            builder.Property(t => t.OldValue)
                .IsRequired();
            builder.Property(m => m.NewValue)
                .IsRequired();
            builder.HasIndex(m => m.AuditLogId)
                .HasName("IX_AuditDetail_AuditLog");
            builder.HasOne(m => m.AuditLog)
                .WithMany(m => m.AuditDetails)
                .HasForeignKey(m => m.AuditLogId);
        }
    }
}
