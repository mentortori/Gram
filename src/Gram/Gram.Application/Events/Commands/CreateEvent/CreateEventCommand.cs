using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest
    {
        public string EventName { get; set; }
        public int EventStatusId { get; set; }
        public string EventDescription { get; set; }
        public DateTime? EventDate { get; set; }

        public class Handler : IRequestHandler<CreateEventCommand, Unit>
        {
            private readonly IDataContext _context;
            private readonly IMediator _mediator;

            public Handler(IDataContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
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
