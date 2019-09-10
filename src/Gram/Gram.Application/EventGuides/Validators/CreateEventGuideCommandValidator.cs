﻿using FluentValidation;
using Gram.Application.EventGuides.Models;

namespace Gram.Application.Attendees.Validators
{
    public class CreateEventGuideCommandValidator : AbstractValidator<EventGuideCreateModel>
    {
        public CreateEventGuideCommandValidator()
        {
            RuleFor(m => m.GuideId)
                .NotEmpty().WithMessage("Guide is required!");
        }
    }
}