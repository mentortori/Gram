using Gram.Persistence.Audit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Audit.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLog", "Audit");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.EntityState)
                .IsFixedLength()
                .HasMaxLength(1);
            builder.Property(t => t.Entity)
                .HasMaxLength(128)
                .IsRequired();
            builder.Property(m => m.RowModifyUser)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(m => m.RowModifyDate)
                .IsRequired();
        }
    }
}
