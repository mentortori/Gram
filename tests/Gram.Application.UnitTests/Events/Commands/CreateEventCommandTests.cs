using Gram.Application.Events.Commands;
using Gram.Tests.Common.Abstraction;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Gram.Application.UnitTests.Events.Commands
{
    public class CreateEventCommandTests : BaseTest
    {
        [Fact]
        public async Task Handler_ShouldPersistEvent_GivenAnyInput()
        {
            // Arrange
            var model = new CreateEventCommand.CreateModel();
            var command = new CreateEventCommand(model);
            var sut = new CreateEventCommand.Handler(DataContext);

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            var entity = await DataContext.Events.FindAsync(result);
            entity.ShouldNotBeNull();
        }

        [InlineData(false, null, 0, null)]
        [InlineData(false, "", 0, null)]
        [InlineData(true, nameof(CreateEventCommand.CreateModel.EventName), 1, nameof(CreateEventCommand.CreateModel.EventDescription))]
        [Theory]
        public async Task Validator_ShouldValidateCreateModel_GivenAnyInput(bool expectedResult, string eventName, int eventStatusId, string eventDescription)
        {
            // Arrange
            var model = new CreateEventCommand.CreateModel
            {
                EventName = eventName,
                EventStatusId = eventStatusId,
                EventDescription = eventDescription
            };

            var sut = new CreateEventCommand.Validator();

            // Act
            var result = await sut.ValidateAsync(model);

            // Assert
            result.IsValid.ShouldBe(expectedResult);
        }
    }
}
