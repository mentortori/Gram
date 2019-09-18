using FluentValidation;
using Gram.Application.Employees.Models;

namespace Gram.Application.Employees.Validators
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<EmployeeEditModel>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(m => m.PersonId)
                .NotEmpty().WithMessage("Person is required!");
        }
    }
}
