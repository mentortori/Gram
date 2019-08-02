using Gram.Persistence.Extensions;
using Gram.Web.Areas.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gram.Web.Areas.Identity
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
            //modelBuilder.ChangeOnDeleteConvention();
        }
    }
}
