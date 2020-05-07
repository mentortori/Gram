using Gram.Application.UnitTests.Common;
using Gram.Persistence;
using System;

namespace Gram.Application.UnitTests.Abstraction
{
    public class BaseTest : IDisposable
    {
        private readonly AuditContext _auditContext;
        protected readonly DataContext DataContext;

        protected BaseTest()
        {
            var databaseName = Guid.NewGuid().ToString();
            _auditContext = AuditContextFactory.Create(databaseName);
            DataContext = DataContextFactory.Create(databaseName, _auditContext);
        }

        public void Dispose()
        {
            DataContextFactory.Destroy(DataContext);
            AuditContextFactory.Destroy(_auditContext);
        }
    }
}
