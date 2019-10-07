using FluentValidation;
using Gram.Application.Employees.Models;

namespace Gram.Application.Employees.Validators
{
    public class EmployeeEditModelValidator : AbstractValidator<EmployeeEditModel>
    {
        public EmployeeEditModelValidator()
        {
            RuleFor(m => m.PersonId)
                .NotEmpty().WithMessage("Person is required!");
        }
    }
}
