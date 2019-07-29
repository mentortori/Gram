using FluentValidation;

namespace Gram.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(m => m.EventName)
                .NotEmpty().WithMessage("Event name is required!")
                .MaximumLength(50).WithMessage("Event name cannot be longer than 50 characters!");

            RuleFor(m => m.EventStatusId)
                .NotEmpty().WithMessage("Event status is required!");

            RuleFor(m => m.EventDescription)
                .NotEmpty().WithMessage("Event description is required!")
                .MaximumLength(4000).WithMessage("Event description cannot be longer than 4000 characters!");
        }
    }
}
