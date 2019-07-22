using FluentValidation;
using Gram.Core.Entities;

namespace Gram.Core.Validators
{
    public class ParticipationValidator : AbstractValidator<Participation>
    {
        public ParticipationValidator()
        {
            RuleFor(m => m.EventId)
                .NotEmpty().WithMessage("Participating event is required!");

            RuleFor(m => m.PersonId)
                .NotEmpty().WithMessage("Participating person is required!");

            RuleFor(m => m.StatusId)
                .NotEmpty().WithMessage("Participation status is required!");

            RuleFor(m => m.StatusDate)
                .NotEmpty().WithMessage("Participation status date is required!");

            RuleFor(m => m.Remarks)
                .MaximumLength(50).WithMessage("Remarks cannot be longer than 50 characters!");
        }
    }
}
