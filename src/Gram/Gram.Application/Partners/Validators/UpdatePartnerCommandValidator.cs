using FluentValidation;
using Gram.Application.Partners.Models;

namespace Gram.Application.Partners.Validators
{
    public class UpdatePartnerCommandValidator : AbstractValidator<PartnerEditModel>
    {
        public UpdatePartnerCommandValidator()
        {
            RuleFor(m => m.PersonId)
                .NotEmpty().WithMessage("Person is required!");

            RuleFor(m => m.IsActive)
                .NotEmpty().WithMessage("Is guide active is required!");
        }
    }
}
