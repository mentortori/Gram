using FluentValidation;
using Gram.Application.Attendees.Models;

namespace Gram.Application.Attendees.Validators
{
    public class UpdateAttendanceCommandValidator : AbstractValidator<AttendanceEditModel>
    {
        public UpdateAttendanceCommandValidator()
        {
            RuleFor(m => m.StatusId)
                .NotEmpty().WithMessage("Attendance status is required!");

            RuleFor(m => m.StatusDate)
                .NotEmpty().WithMessage("Attendance status date is required!");

            RuleFor(m => m.Remarks)
                .MaximumLength(50).WithMessage("Remarks cannot be longer than 50 characters!");
        }
    }
}
