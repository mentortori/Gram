using FluentValidation;
using Gram.Application.Partners.Models;

namespace Gram.Application.Partners.Validators
{
    public class CreatePartnerCommandValidator : AbstractValidator<PartnerCreateModel>
    {
        public CreatePartnerCommandValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Name is required!");

            RuleFor(m => m.IsActive)
                .NotEmpty().WithMessage("Is partner active is required!");
        }
    }
}
