using FluentValidation;
using Gram.Application.Events.Models;

namespace Gram.Application.Events.Validators
{
    public class EventEditModelValidator : AbstractValidator<EventEditModel>
    {
        public EventEditModelValidator()
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
