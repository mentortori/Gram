using FluentValidation;
using Gram.Application.ContactDetails.Models;

namespace Gram.Application.ContactDetails.Validators
{
    public class ContactDetailsUpdateModelValidator : AbstractValidator<ContactDetailsUpdateModel>
    {
        public ContactDetailsUpdateModelValidator()
        {
            RuleFor(m => m.Mobile)
                .MaximumLength(100).WithMessage("Mobile number cannot be longer than 100 characters!");

            RuleFor(m => m.Email)
                .MaximumLength(100).WithMessage("Email address cannot be longer than 100 characters!");

            RuleFor(m => m.Facebook)
                .MaximumLength(100).WithMessage("Facebook URL cannot be longer than 100 characters!");

            RuleFor(m => m.Instagram)
                .MaximumLength(100).WithMessage("Instagram URL number cannot be longer than 100 characters!");

            RuleFor(m => m.Twitter)
                .MaximumLength(100).WithMessage("Twitter URL number cannot be longer than 100 characters!");

            RuleFor(m => m.Web)
                .MaximumLength(100).WithMessage("Web URL number cannot be longer than 100 characters!");
        }
    }
}
