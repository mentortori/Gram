using Gram.Application.Events.Commands.CreateEventCommand;
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
            var model = new Model();
            var command = new Command(model);
            var sut = new Command.Handler(DataContext);

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            var entity = await DataContext.Events.FindAsync(result);
            entity.ShouldNotBeNull();
        }

        [InlineData(false, null, 0, null)]
        [InlineData(false, "", 0, null)]
        [InlineData(true, nameof(Model.EventName), 1, nameof(Model.EventDescription))]
        [Theory]
        public async Task Validator_ShouldValidateModel_GivenAnyInput(bool expectedResult, string eventName, int eventStatusId, string eventDescription)
        {
            // Arrange
            var model = new Model
            {
                EventName = eventName,
                EventStatusId = eventStatusId,
                EventDescription = eventDescription
            };

            var sut = new Validator();

            // Act
            var result = await sut.ValidateAsync(model);

            // Assert
            result.IsValid.ShouldBe(expectedResult);
        }
    }
}
