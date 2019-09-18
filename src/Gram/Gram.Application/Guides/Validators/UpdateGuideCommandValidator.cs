using FluentValidation;
using Gram.Application.Guides.Models;

namespace Gram.Application.Guides.Validators
{
    public class UpdateGuideCommandValidator : AbstractValidator<GuideEditModel>
    {
        public UpdateGuideCommandValidator()
        {
            RuleFor(m => m.PersonId)
                .NotEmpty().WithMessage("Person is required!");

            RuleFor(m => m.IsActive)
                .NotEmpty().WithMessage("Is guide active is required!");
        }
    }
}
