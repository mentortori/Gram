using Gram.Application.Abstraction;
using Gram.Application.EventPartners.Models;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.EventPartners.Commands
{
    public class CreateEventPartnerCommand : IRequest
    {
        private int EventId { get; }
        private EventPartnerCreateModel Model { get; }

        public CreateEventPartnerCommand(int eventId, EventPartnerCreateModel model)
        {
            EventId = eventId;
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<CreateEventPartnerCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(CreateEventPartnerCommand request, CancellationToken cancellationToken)
            {
                var entity = new EventPartner
                {
                    EventId = request.EventId,
                    PartnerId = request.Model.PartnerId
                };

                await DataContext.EventPartners.AddAsync(entity, cancellationToken);
                await DataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
