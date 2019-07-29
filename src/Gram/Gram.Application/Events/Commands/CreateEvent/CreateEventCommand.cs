using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest
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

        public class Handler : IRequestHandler<CreateEventCommand, Unit>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken)
            {
                var entity = new Event
                {
                    EventName = request.EventName,
                    EventStatusId = request.EventStatusId,
                    EventDescription = request.EventDescription,
                    EventDate = request.EventDate
                };

                _context.Events.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
