using FluentValidation;
using Gram.Application.Abstraction;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Commands
{
    public class CreateEventCommand : IRequest<int>
    {
        private CreateModel Model { get; }

        public CreateEventCommand(CreateModel model)
        {
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<CreateEventCommand, int>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken)
            {
                var entity = new Event
                {
                    EventName = request.Model.EventName,
                    EventStatusId = request.Model.EventStatusId,
                    EventDescription = request.Model.EventDescription,
                    EventDate = request.Model.EventDate
                };

                await DataContext.Events.AddAsync(entity, cancellationToken);
                await DataContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }

        public class CreateModel
        {
            [Display(Name = "Event name")]
            public string EventName { get; set; }

            [Display(Name = "Status")]
            public int EventStatusId { get; set; }

            [Display(Name = "Event description")]
            [DataType(DataType.MultilineText)]
            public string EventDescription { get; set; }

            [Display(Name = "Event date")]
            [DataType(DataType.Date)]
            public DateTime? EventDate { get; set; }
        }

        public class Validator : AbstractValidator<CreateModel>
        {
            public Validator()
            {
                RuleFor(m => m.EventName)
                    .NotEmpty().WithMessage("Event name is required!")
                    .MaximumLength(50).WithMessage("Event name cannot be longer than 50 characters!");

                RuleFor(m => m.EventStatusId)
                    .NotEmpty().WithMessage("Event status is required!");

                RuleFor(m => m.EventDescription)
                    .NotEmpty().WithMessage("Event description is required!")
                    .MaximumLength(4000).WithMessage("Event description cannot be longer than 4000 characters!");
            }
        }
    }
}
