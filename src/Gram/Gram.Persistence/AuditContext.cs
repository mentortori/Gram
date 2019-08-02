using Gram.Domain.Interfaces;
using Gram.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gram.Persistence.Services
{
    public class AuditContext : DbContext, IAuditContext
    {
        public AuditContext(DbContextOptions<AuditContext> options, IUserService userService)
            : base(options)
        {
            _auditLogs = new List<AuditLog>();
            _userService = userService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AuditDetailConfiguration());
            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
        }

        internal virtual DbSet<AuditDetail> AuditDetails { get; set; }
        internal virtual DbSet<AuditLog> AuditLogs { get; set; }

        private static readonly string[] _excludedProperties = new[] { "Id", "RowVersion" };
        private IUserService _userService { get; }
        private List<EntityEntry<IEntity>> _addedEntries { get; set; }
        private List<EntityEntry<IEntity>> _modifiedEntries { get; set; }
        private List<EntityEntry<IEntity>> _deletedEntries { get; set; }
        private List<AuditDetail> _auditDetails { get; set; }
        private List<AuditLog> _auditLogs { get; set; }

        public async Task DetectChangesAsync(ChangeTracker changeTracker)
        {
            _addedEntries = changeTracker.Entries<IEntity>().Where(p => p.State == EntityState.Added).ToList();
            _modifiedEntries = changeTracker.Entries<IEntity>().Where(p => p.State == EntityState.Modified).ToList();
            _deletedEntries = changeTracker.Entries<IEntity>().Where(p => p.State == EntityState.Deleted).ToList();
            await AuditUpdatesAsync();
            await AuditDeletesAsync();
        }

        public async Task AuditAsync()
        {
            foreach (var entry in _addedEntries)
            {
                var newEntityValues = entry.CurrentValues;

                var log = new AuditLog
                {
                    EntityState = "A",
                    Entity = $"{ entry.Metadata.Relational().Schema }.{ entry.Metadata.Relational().TableName }",
                    EntityId = entry.Entity.Id,
                    RowModifyUser = _userService.GetCurrentUser(),
                    RowModifyDate = DateTime.UtcNow,
                    AuditDetails = new List<AuditDetail>()
                };

                foreach (var property in newEntityValues.Properties.Where(p => !_excludedProperties.Contains(p.Name)))
                {
                    var newValue = newEntityValues[property];

                    if (newValue == null)
                        continue;

                    var detail = new AuditDetail
                    {
                        Property = property.Name,
                        OldValue = "",
                        NewValue = newValue.ToString()
                    };

                    log.AuditDetails.Add(detail);
                }

                if (log.AuditDetails.Any())
                    _auditLogs.Add(log);
            }

            if (_auditLogs.Any())
            {
                await AuditLogs.AddRangeAsync(_auditLogs);
                await SaveChangesAsync();
            }
        }

        private async Task AuditUpdatesAsync()
        {
            foreach (var entry in _modifiedEntries)
            {
                var oldEntityValues = await entry.GetDatabaseValuesAsync();
                var newEntityValues = entry.CurrentValues;

                var log = new AuditLog
                {
                    EntityState = "M",
                    Entity = $"{ entry.Metadata.Relational().Schema }.{ entry.Metadata.Relational().TableName }",
                    EntityId = entry.Entity.Id,
                    RowModifyUser = _userService.GetCurrentUser(),
                    RowModifyDate = DateTime.UtcNow,
                    AuditDetails = new List<AuditDetail>()
                };

                foreach (var property in newEntityValues.Properties.Where(p => !_excludedProperties.Contains(p.Name)))
                {
                    var oldValue = oldEntityValues[property.Name];
                    var newValue = newEntityValues[property.Name];

                    if (oldValue == null && newValue == null)
                        continue;

                    if (oldValue != null && newValue != null && oldValue.Equals(newValue))
                        continue;

                    var detail = new AuditDetail
                    {
                        Property = property.Name,
                        OldValue = oldValue?.ToString() ?? "",
                        NewValue = newValue?.ToString() ?? ""
                    };

                    log.AuditDetails.Add(detail);
                }

                if (log.AuditDetails.Any())
                    _auditLogs.Add(log);
            }
        }

        private async Task AuditDeletesAsync()
        {
            foreach (var entry in _deletedEntries)
            {
                var oldEntityValues = await entry.GetDatabaseValuesAsync();

                var log = new AuditLog
                {
                    EntityState = "D",
                    Entity = $"{ entry.Metadata.Relational().Schema }.{ entry.Metadata.Relational().TableName }",
                    EntityId = entry.Entity.Id,
                    RowModifyUser = _userService.GetCurrentUser(),
                    RowModifyDate = DateTime.UtcNow,
                    AuditDetails = new List<AuditDetail>()
                };

                foreach (var property in oldEntityValues.Properties.Where(p => !_excludedProperties.Contains(p.Name)))
                {
                    var oldValue = oldEntityValues[property.Name];

                    if (oldValue == null)
                        continue;

                    var detail = new AuditDetail
                    {
                        Property = property.Name,
                        OldValue = oldValue?.ToString() ?? "",
                        NewValue = ""
                    };

                    log.AuditDetails.Add(detail);
                }

                if (log.AuditDetails.Any())
                    _auditLogs.Add(log);
            }
        }

        internal class AuditDetail
        {
            public int Id { get; set; }
            public int AuditLogId { get; set; }
            public string Property { get; set; }
            public string OldValue { get; set; }
            public string NewValue { get; set; }

            public virtual AuditLog AuditLog { get; set; }
        }

        internal class AuditLog
        {
            public int Id { get; set; }
            public string EntityState { get; set; }
            public string Entity { get; set; }
            public int EntityId { get; set; }
            public string RowModifyUser { get; set; }
            public DateTime RowModifyDate { get; set; }

            public virtual ICollection<AuditDetail> AuditDetails { get; set; }
        }

        internal class AuditDetailConfiguration : IEntityTypeConfiguration<AuditDetail>
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

        internal class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
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
}
