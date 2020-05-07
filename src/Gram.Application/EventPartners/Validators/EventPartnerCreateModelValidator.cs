using FluentValidation;
using Gram.Application.EventPartners.Models;

namespace Gram.Application.EventPartners.Validators
{
    public class EventPartnerCreateModelValidator : AbstractValidator<EventPartnerCreateModel>
    {
        public EventPartnerCreateModelValidator()
        {
            RuleFor(m => m.PartnerId)
                .NotEmpty().WithMessage("Partner is required!");
        }
    }
}
