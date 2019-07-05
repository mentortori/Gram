using FluentValidation;
using Gram.Core.Entities;

namespace Gram.Core.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
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
