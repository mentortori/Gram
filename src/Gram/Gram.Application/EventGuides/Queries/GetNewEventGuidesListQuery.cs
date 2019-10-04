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
    public class GetNewEventGuidesListQuery : IRequest<List<ListItemModel>>
    {
        private int EventId { get; }

        public GetNewEventGuidesListQuery(int eventId)
        {
            EventId = eventId;
        }

        public class Handler : BaseHandler, IRequestHandler<GetNewEventGuidesListQuery, List<ListItemModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<ListItemModel>> Handle(GetNewEventGuidesListQuery request, CancellationToken cancellationToken)
            {
                var existing = await DataContext.EventGuides.Where(m => m.EventId == request.EventId)
                    .Select(m => m.GuideId)
                    .ToArrayAsync(cancellationToken);

                return await DataContext.Guides
                    .Where(m => m.IsActive)
                    .Where(m => !existing.Contains(m.Id))
                    .Include(m => m.Person)
                    .Select(m => new ListItemModel
                    {
                        Id = m.Id,
                        Name = m.Person.FirstName + " " + m.Person.LastName
                    })
                    .OrderBy(m => m.Name)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
