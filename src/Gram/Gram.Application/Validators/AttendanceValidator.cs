using FluentValidation;
using Gram.Domain.Entities;

namespace Gram.Application.Validators
{
    public class AttendanceValidator : AbstractValidator<Attendance>
    {
        public AttendanceValidator()
        {
            RuleFor(m => m.EventId)
                .NotEmpty().WithMessage("Participating event is required!");

            RuleFor(m => m.PersonId)
                .NotEmpty().WithMessage("Participating person is required!");

            RuleFor(m => m.StatusId)
                .NotEmpty().WithMessage("Attendance status is required!");

            RuleFor(m => m.StatusDate)
                .NotEmpty().WithMessage("Attendance status date is required!");

            RuleFor(m => m.Remarks)
                .MaximumLength(50).WithMessage("Remarks cannot be longer than 50 characters!");
        }
    }
}
