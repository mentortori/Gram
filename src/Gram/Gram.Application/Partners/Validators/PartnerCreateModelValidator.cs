using FluentValidation;
using Gram.Application.Partners.Models;

namespace Gram.Application.Partners.Validators
{
    public class PartnerCreateModelValidator : AbstractValidator<PartnerCreateModel>
    {
        public PartnerCreateModelValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Name is required!");

            RuleFor(m => m.IsActive)
                .NotEmpty().WithMessage("Is partner active is required!");
        }
    }
}
