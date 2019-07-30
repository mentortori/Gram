using Gram.Application.Abstraction;
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

        public class Handler : BaseHandler, IRequestHandler<CreateEventCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
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

                DataContext.Events.Add(entity);
                await DataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}