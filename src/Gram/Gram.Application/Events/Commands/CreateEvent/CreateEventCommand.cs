using Gram.Application.Events.Models;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest
    {
        public EventCreateModel EventCreateModel { get; set; }

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
                    EventName = request.EventCreateModel.EventName,
                    EventStatusId = request.EventCreateModel.EventStatusId,
                    EventDescription = request.EventCreateModel.EventDescription,
                    EventDate = request.EventCreateModel.EventDate
                };

                _context.Events.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}