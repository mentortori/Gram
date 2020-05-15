using Gram.Application.Interfaces;
using Gram.Domain.Interfaces;
using Gram.Persistence.Audit.Configurations;
using Gram.Persistence.Audit.Models;
using Gram.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gram.Persistence.Audit
{
    public class AuditContext : DbContext, IAuditContext
    {
        private static readonly string[] ExcludedProperties = { "Id", "RowVersion" };
        private readonly IUserService _userService;
        private List<EntityEntry<IEntity>> _addedEntries;
        private readonly List<AuditLog> _newAuditLogs;

        public DbSet<AuditLog> AuditLogs { get; set; }

        public AuditContext(DbContextOptions<AuditContext> options, IUserService userService)
            : base(options)
        {
            _newAuditLogs = new List<AuditLog>();
            _userService = userService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AuditDetailConfiguration());
            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
            modelBuilder.ChangeOnDeleteConvention();
        }

        public async Task DetectChangesAsync(ChangeTracker changeTracker)
        {
            _addedEntries = changeTracker.Entries<IEntity>().Where(p => p.State == EntityState.Added).ToList();
            await DetectUpdatesAsync(changeTracker.Entries<IEntity>().Where(p => p.State == EntityState.Modified).ToList());
            await DetectDeletesAsync(changeTracker.Entries<IEntity>().Where(p => p.State == EntityState.Deleted).ToList());
        }

        public async Task SaveAuditLogAsync()
        {
            foreach (var entry in _addedEntries)
            {
                var newEntityValues = entry.CurrentValues;

                var log = new AuditLog
                {
                    EntityState = "A",
                    Entity = $"{ entry.Metadata.GetSchema() }.{ entry.Metadata.GetTableName() }",
                    EntityId = entry.Entity.Id,
                    RowModifyUser = _userService.UserName,
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
                    _newAuditLogs.Add(log);
            }

            if (_newAuditLogs.Any())
            {
                await AuditLogs.AddRangeAsync(_newAuditLogs);
                await SaveChangesAsync();
            }
        }

        private async Task DetectUpdatesAsync(List<EntityEntry<IEntity>> updatedEntries)
        {
            foreach (var entry in updatedEntries)
            {
                var oldEntityValues = await entry.GetDatabaseValuesAsync();
                var newEntityValues = entry.CurrentValues;

                var log = new AuditLog
                {
                    EntityState = "M",
                    Entity = $"{ entry.Metadata.GetSchema() }.{ entry.Metadata.GetTableName() }",
                    EntityId = entry.Entity.Id,
                    RowModifyUser = _userService.UserName,
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
                    _newAuditLogs.Add(log);
            }
        }

        private async Task DetectDeletesAsync(List<EntityEntry<IEntity>> deletedEntries)
        {
            foreach (var entry in deletedEntries)
            {
                var oldEntityValues = await entry.GetDatabaseValuesAsync();

                var log = new AuditLog
                {
                    EntityState = "D",
                    Entity = $"{ entry.Metadata.GetSchema() }.{ entry.Metadata.GetTableName() }",
                    EntityId = entry.Entity.Id,
                    RowModifyUser = _userService.UserName,
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
                    _newAuditLogs.Add(log);
            }
        }
    }
}
