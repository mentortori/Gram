using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using Gram.Persistence.Configurations;
using Gram.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Persistence
{
    public sealed class DataContext : DbContext, IDataContext
    {
        private readonly IAuditContext _auditContext;

        public DataContext(DbContextOptions<DataContext> options, IAuditContext auditContext)
            : base(options)
        {
            _auditContext = auditContext;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyBaseEntityConfigurations();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly, p => p.Namespace == typeof(BaseEntityConfiguration<>).Namespace);
            modelBuilder.ChangeOnDeleteConvention();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _auditContext.DetectChangesAsync(ChangeTracker);
            var result = await base.SaveChangesAsync(cancellationToken);
            await _auditContext.AuditAsync();
            return result;
        }

        public DbSet<Attendance> Attendees { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventGuide> EventGuides { get; set; }
        public DbSet<EventPartner> EventPartners { get; set; }
        public DbSet<GeneralType> GeneralTypes { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonContactInfo> PersonContactInfos { get; set; }
    }
}
