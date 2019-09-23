using FluentValidation;
using Gram.Application.Partners.Models;

namespace Gram.Application.Partners.Validators
{
    public class CreatePartnerCommandValidator : AbstractValidator<PartnerCreateModel>
    {
        public CreatePartnerCommandValidator()
        {
            RuleFor(m => m.PersonId)
                .NotEmpty().WithMessage("Person is required!");

            RuleFor(m => m.IsActive)
                .NotEmpty().WithMessage("Is guide active is required!");
        }
    }
}
