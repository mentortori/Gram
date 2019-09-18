using FluentValidation;
using Gram.Application.Employees.Models;

namespace Gram.Application.Employees.Validators
{
    public class CreateEmployeeCommandValidator : AbstractValidator<EmployeeCreateModel>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(m => m.PersonId)
                .NotEmpty().WithMessage("Person is required!");
        }
    }
}
