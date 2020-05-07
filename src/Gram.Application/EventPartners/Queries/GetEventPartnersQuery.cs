using Gram.Application.Abstraction;
using Gram.Application.EventPartners.Models;
using Gram.Application.Interfaces;
using Gram.Application.SharedModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.EventPartners.Queries
{
    public class GetEventPartnersQuery : IRequest<EventPartnersViewModel>
    {
        private int EventId { get; }

        public GetEventPartnersQuery(int eventId)
        {
            EventId = eventId;
        }

        public class Handler : BaseHandler, IRequestHandler<GetEventPartnersQuery, EventPartnersViewModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<EventPartnersViewModel> Handle(GetEventPartnersQuery request, CancellationToken cancellationToken) =>
                new EventPartnersViewModel
                {
                    EventId = request.EventId,
                    Partners = await DataContext.EventPartners
                        .Where(m => m.EventId == request.EventId)
                        .Select(m => new ListItemWithRowVersionModel
                        {
                            Id = m.Id,
                            Name = m.Partner.Name,
                            RowVersion = m.RowVersion
                        })
                        .OrderBy(m => m.Name)
                        .ToListAsync(cancellationToken)
                };
        }
    }
}
