using Gram.Persistence.Audit;
using Gram.Persistence.Data;
using System;

namespace Gram.Tests.Common.Abstraction
{
    public class BaseTest : IDisposable
    {
        protected readonly AuditContext AuditContext;
        protected readonly DataContext DataContext;

        protected BaseTest()
        {
            var databaseName = Guid.NewGuid().ToString();
            AuditContext = AuditContextFactory.Create(databaseName);
            DataContext = DataContextFactory.Create(databaseName, AuditContext);
        }

        public void Dispose()
        {
            AuditContextFactory.Destroy(AuditContext);
            DataContextFactory.Destroy(DataContext);
        }
    }
}
