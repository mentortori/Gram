using Gram.Domain.Entities;
using Gram.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gram.Tests.Common
{
    public static class DataContextFactory
    {
        public static DataContext Create(string databaseName, AuditContext auditContext)
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName).Options;
            var context = new DataContext(options, auditContext);
            context.Database.EnsureCreated();
            SeedSampleData(context);
            return context;
        }

        public static void Destroy(DataContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private static void SeedSampleData(DataContext context)
        {
            var model = new Event
            {
                EventName = nameof(Event.EventName),
                EventStatusId = 1,
                EventDescription = nameof(Event.EventDescription)
            };

            context.Events.Add(model);
            context.SaveChanges();
        }
    }
}
