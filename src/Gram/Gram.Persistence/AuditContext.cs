using Gram.Application.Interfaces;
using Gram.Domain.Interfaces;
using Gram.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gram.Persistence
{
    public sealed class AuditContext : DbContext, IAuditContext
    {
        public AuditContext(DbContextOptions<AuditContext> options, IUserService userService)
            : base(options)
        {
            NewAuditLogs = new List<AuditLog>();
            UserService = userService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AuditDetailConfiguration());
            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
            modelBuilder.ChangeOnDeleteConvention();
        }

        internal DbSet<AuditDetail> AuditDetails { get; set; }
        private DbSet<AuditLog> AuditLogs { get; set; }

        private static readonly string[] ExcludedProperties = { "Id", "RowVersion" };
        private IUserService UserService { get; }
        private List<EntityEntry<IEntity>> AddedEntries { get; set; }
        private List<EntityEntry<IEntity>> ModifiedEntries { get; set; }
        private List<EntityEntry<IEntity>> DeletedEntries { get; set; }
        private List<AuditLog> NewAuditLogs { get; }

        public async Task DetectChangesAsync(ChangeTracker changeTracker)
        {
            AddedEntries = changeTracker.Entries<IEntity>().Where(p => p.State == EntityState.Added).ToList();
            ModifiedEntries = changeTracker.Entries<IEntity>().Where(p => p.State == EntityState.Modified).ToList();
            DeletedEntries = changeTracker.Entries<IEntity>().Where(p => p.State == EntityState.Deleted).ToList();
            await AuditUpdatesAsync();
            await AuditDeletesAsync();
        }

        public async Task AuditAsync()
        {
            foreach (var entry in AddedEntries)
            {
                var newEntityValues = entry.CurrentValues;

                var log = new AuditLog
                {
                    EntityState = "A",
                    Entity = $"{ entry.Metadata.GetSchema() }.{ entry.Metadata.GetTableName() }",
                    EntityId = entry.Entity.Id,
                    RowModifyUser = UserService.UserName,
                    RowModifyDate = DateTime.UtcNow,
                    AuditDetails = new List<AuditDetail>()
                };

                foreach (var property in newEntityValues.Properties.Where(p => !ExcludedProperties.Contains(p.Name)))
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
                    NewAuditLogs.Add(log);
            }

            if (NewAuditLogs.Any())
            {
                await AuditLogs.AddRangeAsync(NewAuditLogs);
                await SaveChangesAsync();
            }
        }

        private async Task AuditUpdatesAsync()
        {
            foreach (var entry in ModifiedEntries)
            {
                var oldEntityValues = await entry.GetDatabaseValuesAsync();
                var newEntityValues = entry.CurrentValues;

                var log = new AuditLog
                {
                    EntityState = "M",
                    Entity = $"{ entry.Metadata.GetSchema() }.{ entry.Metadata.GetTableName() }",
                    EntityId = entry.Entity.Id,
                    RowModifyUser = UserService.UserName,
                    RowModifyDate = DateTime.UtcNow,
                    AuditDetails = new List<AuditDetail>()
                };

                foreach (var property in newEntityValues.Properties.Where(p => !ExcludedProperties.Contains(p.Name)))
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
                    NewAuditLogs.Add(log);
            }
        }

        private async Task AuditDeletesAsync()
        {
            foreach (var entry in DeletedEntries)
            {
                var oldEntityValues = await entry.GetDatabaseValuesAsync();

                var log = new AuditLog
                {
                    EntityState = "D",
                    Entity = $"{ entry.Metadata.GetSchema() }.{ entry.Metadata.GetTableName() }",
                    EntityId = entry.Entity.Id,
                    RowModifyUser = UserService.UserName,
                    RowModifyDate = DateTime.UtcNow,
                    AuditDetails = new List<AuditDetail>()
                };

                foreach (var property in oldEntityValues.Properties.Where(p => !ExcludedProperties.Contains(p.Name)))
                {
                    var oldValue = oldEntityValues[property.Name];

                    if (oldValue == null)
                        continue;

                    var detail = new AuditDetail
                    {
                        Property = property.Name,
                        OldValue = oldValue.ToString(),
                        NewValue = ""
                    };

                    log.AuditDetails.Add(detail);
                }

                if (log.AuditDetails.Any())
                    NewAuditLogs.Add(log);
            }
        }

        internal sealed class AuditDetail
        {
            public int Id { get; set; }
            public int AuditLogId { get; set; }
            public string Property { get; set; }
            public string OldValue { get; set; }
            public string NewValue { get; set; }

            public AuditLog AuditLog { get; set; }
        }

        internal sealed class AuditLog
        {
            public AuditLog()
            {
                AuditDetails = new HashSet<AuditDetail>();
            }

            public int Id { get; set; }
            public string EntityState { get; set; }
            public string Entity { get; set; }
            public int EntityId { get; set; }
            public string RowModifyUser { get; set; }
            public DateTime RowModifyDate { get; set; }

            public ICollection<AuditDetail> AuditDetails { get; set; }
        }

        private class AuditDetailConfiguration : IEntityTypeConfiguration<AuditDetail>
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

        private class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
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
