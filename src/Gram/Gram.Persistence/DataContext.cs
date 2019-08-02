using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using Gram.Persistence.Configurations;
using Gram.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Persistence
{
    public class DataContext : DbContext, IDataContext
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

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<GeneralType> GeneralTypes { get; set; }
        public virtual DbSet<Participation> Participations { get; set; }
        public virtual DbSet<Person> People { get; set; }
    }
}
