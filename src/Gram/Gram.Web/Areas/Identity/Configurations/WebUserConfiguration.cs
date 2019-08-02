using Gram.Web.Areas.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Web.Areas.Identity.Configurations
{
    public class WebUserConfiguration : IEntityTypeConfiguration<WebUser>
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
