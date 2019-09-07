using FluentValidation;
using Gram.Application.Guides.Models;

namespace Gram.Application.Guides.Validators
{
    public class CreateGuideCommandValidator : AbstractValidator<GuideCreateModel>
    {
        public CreateGuideCommandValidator()
        {
            RuleFor(m => m.PersonId)
                .NotEmpty().WithMessage("Person is required!");

            RuleFor(m => m.IsActive)
                .NotEmpty().WithMessage("Is guide active is required!");
        }
    }
}
