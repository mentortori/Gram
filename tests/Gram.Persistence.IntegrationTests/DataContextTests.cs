using Gram.Domain.Entities;
using Gram.Tests.Common.Abstraction;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Gram.Persistence.IntegrationTests
{
    public class DataContextTests : BaseTest
    {
        [Fact]
        public async Task SaveChangesAsync_ShouldCreateAuditLog_AddingEvent()
        {
            // Arrange
            var model = new Event
            {
                EventName = nameof(Event.EventName),
                EventStatusId = 1,
                EventDescription = nameof(Event.EventDescription),
            };

            // Act
            await DataContext.Events.AddAsync(model);
            await DataContext.SaveChangesAsync();

            // Assert
            var auditLog = await AuditContext.AuditLogs.SingleAsync(m => m.EntityId == model.Id && m.EntityState == "A");
            var auditDetails = auditLog.AuditDetails;
            auditLog.ShouldNotBeNull();
            auditDetails.Count.ShouldBe(3);
            auditDetails.Any(m => m.OldValue == null).ShouldBeFalse();
            auditDetails.Single(m => m.Property == nameof(Event.EventName)).NewValue.ShouldBe(model.EventName);
            auditDetails.Single(m => m.Property == nameof(Event.EventStatusId)).NewValue.ShouldBe(model.EventStatusId.ToString());
            auditDetails.Single(m => m.Property == nameof(Event.EventDescription)).NewValue.ShouldBe(model.EventDescription);
        }

        [Fact]
        public async Task SaveChangesAsync_ShouldCreateAuditLog_DeletingEvent()
        {
            // Arrange
            var model = await DataContext.Events.FirstAsync();

            // Act
            DataContext.Events.Remove(model);
            await DataContext.SaveChangesAsync();

            // Assert
            var auditLog = await AuditContext.AuditLogs.SingleAsync(m => m.EntityId == model.Id && m.EntityState == "D");
            var auditDetails = auditLog.AuditDetails;
            auditLog.ShouldNotBeNull();
            auditDetails.Count.ShouldBe(3);
            auditDetails.Single(m => m.Property == nameof(Event.EventName)).OldValue.ShouldBe(model.EventName);
            auditDetails.Single(m => m.Property == nameof(Event.EventStatusId)).OldValue.ShouldBe(model.EventStatusId.ToString());
            auditDetails.Single(m => m.Property == nameof(Event.EventDescription)).OldValue.ShouldBe(model.EventDescription);
        }

        [Fact]
        public async Task SaveChangesAsync_ShouldCreateAuditLog_UpdatingEvent()
        {
            // Arrange
            var model = await DataContext.Events.FirstAsync();
            var initialValue = model.EventName;
            var expectedValue = string.Join("", initialValue.Reverse());

            // Act
            model.EventName = expectedValue;
            await DataContext.SaveChangesAsync();

            // Assert
            var auditLog = await AuditContext.AuditLogs.SingleAsync(m => m.EntityId == model.Id && m.EntityState == "M");
            var auditDetails = auditLog.AuditDetails;
            auditLog.ShouldNotBeNull();
            auditDetails.Count.ShouldBe(1);
            auditDetails.Single(m => m.Property == nameof(Event.EventName)).OldValue.ShouldBe(initialValue);
            auditDetails.Single(m => m.Property == nameof(Event.EventName)).NewValue.ShouldBe(expectedValue);
        }
    }
}
