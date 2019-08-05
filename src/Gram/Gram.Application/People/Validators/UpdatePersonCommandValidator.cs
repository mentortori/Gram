using FluentValidation;
using Gram.Application.People.Models;

namespace Gram.Application.People.Validators
{
    public class UpdatePersonCommandValidator : AbstractValidator<PersonEditModel>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(m => m.FirstName)
                .NotEmpty().WithMessage("First name is required!")
                .MaximumLength(50).WithMessage("First name cannot be longer than 30 characters!");

            RuleFor(m => m.LastName)
                .NotEmpty().WithMessage("Last name is required!")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 30 characters!");
        }
    }
}
