using Gram.Application.Events.Commands;
using Gram.Application.Events.Models;
using Gram.Application.UnitTests.Abstraction;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Gram.Application.UnitTests.Events.Commands
{
    public class CreateEventCommandTests : BaseTest
    {
        [Fact]
        public async Task Handle_ShouldPersistEvent()
        {
            // Arrange
            var model = new EventCreateModel();

            var command = new CreateEventCommand(model);
            var sut = new CreateEventCommand.Handler(DataContext);

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            var entity = await DataContext.Events.FindAsync(result);
            var count = await DataContext.Events.CountAsync();
            entity.ShouldNotBeNull();
            entity.Id.ShouldNotBe(0);
            entity.Id.ShouldBe(count);
        }
    }
}
