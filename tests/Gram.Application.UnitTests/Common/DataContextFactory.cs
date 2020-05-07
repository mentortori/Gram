using Gram.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gram.Application.UnitTests.Common
{
    public static class DataContextFactory
    {
        public static DataContext Create(string databaseName, AuditContext auditContext)
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName).Options;
            var context = new DataContext(options, auditContext);
            context.Database.EnsureCreated();
            return context;
        }

        public static void Destroy(DataContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
