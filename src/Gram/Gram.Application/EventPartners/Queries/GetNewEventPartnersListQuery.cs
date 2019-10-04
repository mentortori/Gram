using Gram.Application.Abstraction;
using Gram.Application.Interfaces;
using Gram.Application.SharedModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.EventGuides.Queries
{
    public class GetNewEventPartnersListQuery : IRequest<List<ListItemModel>>
    {
        private int EventId { get; }

        public GetNewEventPartnersListQuery(int eventId)
        {
            EventId = eventId;
        }

        public class Handler : BaseHandler, IRequestHandler<GetNewEventPartnersListQuery, List<ListItemModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<ListItemModel>> Handle(GetNewEventPartnersListQuery request, CancellationToken cancellationToken)
            {
                var existing = await DataContext.EventPartners.Where(m => m.EventId == request.EventId)
                    .Select(m => m.PartnerId)
                    .ToArrayAsync(cancellationToken);

                return await DataContext.Partners
                    .Where(m => m.IsActive)
                    .Where(m => !existing.Contains(m.Id))
                    .Select(m => new ListItemModel
                    {
                        Id = m.Id,
                        Name = m.Name
                    })
                    .OrderBy(m => m.Name)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
