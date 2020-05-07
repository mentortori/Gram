using Gram.Persistence.Extensions;
using Gram.Persistence.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Persistence.Identity
{
    public class IdentityContext : IdentityDbContext<WebUser>
    {
        public IdentityContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new WebUserConfiguration());
            modelBuilder.ChangeOnDeleteConvention();
        }

        private class WebUserConfiguration : IEntityTypeConfiguration<WebUser>
        {
            public void Configure(EntityTypeBuilder<WebUser> builder)
            {
                builder.Ignore(m => m.Employee);
                builder.HasIndex(m => m.EmployeeId)
                    .HasName("UQ_AspNetUsers_Employee")
                    .IsUnique();
            }
        }
    }
}
