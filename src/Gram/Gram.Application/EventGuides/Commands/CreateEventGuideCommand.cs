using Gram.Application.Abstraction;
using Gram.Application.EventGuides.Models;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.EventGuides.Commands
{
    public class CreateEventGuideCommand : IRequest
    {
        private int EventId { get; }
        private EventGuideCreateModel Model { get; }

        public CreateEventGuideCommand(int eventId, EventGuideCreateModel model)
        {
            EventId = eventId;
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<CreateEventGuideCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(CreateEventGuideCommand request, CancellationToken cancellationToken)
            {
                var entity = new EventGuide
                {
                    EventId = request.EventId,
                    GuideId = request.Model.GuideId
                };

                await DataContext.EventGuides.AddAsync(entity, cancellationToken);
                await DataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
