using FluentValidation;
using Gram.Application.Employees.Models;

namespace Gram.Application.Employees.Validators
{
    public class EmployeeCreateModelValidator : AbstractValidator<EmployeeCreateModel>
    {
        public EmployeeCreateModelValidator()
        {
            RuleFor(m => m.PersonId)
                .NotEmpty().WithMessage("Person is required!");
        }
    }
}
