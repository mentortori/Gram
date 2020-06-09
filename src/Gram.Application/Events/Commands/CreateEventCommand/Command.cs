using Gram.Application.Abstraction;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Commands.CreateEventCommand
{
    public class Command : IRequest<int>
    {
        private Model Model { get; }

        public Command(Model model)
        {
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<Command, int>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
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
