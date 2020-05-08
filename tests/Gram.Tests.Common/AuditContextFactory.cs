using Gram.Application.Interfaces;
using Gram.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Gram.Tests.Common
{
    public static class AuditContextFactory
    {
        public static AuditContext Create(string databaseName)
        {
            var options = new DbContextOptionsBuilder<AuditContext>().UseInMemoryDatabase(databaseName).Options;
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.UserName).Returns("joe@doe.com");
            var context = new AuditContext(options, userServiceMock.Object);
            context.Database.EnsureCreated();
            return context;
        }

        public static void Destroy(AuditContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
