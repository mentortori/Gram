using FluentValidation;
using Gram.Application.EventPartners.Models;

namespace Gram.Application.EventPartners.Validators
{
    public class CreateEventPartnerCommandValidator : AbstractValidator<EventPartnerCreateModel>
    {
        public CreateEventPartnerCommandValidator()
        {
            RuleFor(m => m.PartnerId)
                .NotEmpty().WithMessage("Partner is required!");
        }
    }
}
