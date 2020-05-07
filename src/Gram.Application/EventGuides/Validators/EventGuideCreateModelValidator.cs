using FluentValidation;
using Gram.Application.EventGuides.Models;

namespace Gram.Application.EventGuides.Validators
{
    public class EventGuideCreateModelValidator : AbstractValidator<EventGuideCreateModel>
    {
        public EventGuideCreateModelValidator()
        {
            RuleFor(m => m.GuideId)
                .NotEmpty().WithMessage("Guide is required!");
        }
    }
}
