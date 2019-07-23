using Gram.Core.Entities;
using Gram.Infrastructure.EntityConfigurations;
using Gram.Infrastructure.Extensions;
using Gram.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Infrastructure
{
    public class DataContext : IdentityDbContext
    {
        private readonly IAuditService _auditService;

        public DataContext(DbContextOptions<DataContext> options, IAuditService auditService)
            : base(options)
        {
            _auditService = auditService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyBaseEntityConfigurations();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly, p => p.Namespace == typeof(BaseEntityConfiguration<>).Namespace);
            modelBuilder.ChangeOnDeletePolicy();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _auditService.DetectChangesAsync(ChangeTracker);
            var result = await base.SaveChangesAsync(cancellationToken);
            await _auditService.AuditAsync();
            return result;
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<GeneralType> GeneralTypes { get; set; }
        public virtual DbSet<Participation> Participations { get; set; }
        public virtual DbSet<Person> People { get; set; }
    }
}
