using Gram.Application.Abstraction;
using Gram.Application.Events.Models;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Commands
{
    public class CreateEventCommand : IRequest<int>
    {
        private EventCreateModel Model { get; }

        public CreateEventCommand(EventCreateModel model)
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
    }
}
