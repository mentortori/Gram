using Gram.Application.Abstraction;
using Gram.Application.EventGuides.Models;
using Gram.Application.Interfaces;
using Gram.Application.SharedModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.EventGuides.Queries
{
    public class GetEventGuidesQuery : IRequest<EventGuidesViewModel>
    {
        private int EventId { get; }

        public GetEventGuidesQuery(int eventId)
        {
            EventId = eventId;
        }

        public class Handler : BaseHandler, IRequestHandler<GetEventGuidesQuery, EventGuidesViewModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<EventGuidesViewModel> Handle(GetEventGuidesQuery request, CancellationToken cancellationToken) =>
                new EventGuidesViewModel
                {
                    EventId = request.EventId,
                    Guides = await DataContext.EventGuides
                        .Where(m => m.EventId == request.EventId)
                        .Select(m => new ListItemWithRowVersionModel
                        {
                            Id = m.Id,
                            Name = m.Guide.Person.FirstName + " " + m.Guide.Person.LastName,
                            RowVersion = m.RowVersion
                        })
                        .OrderBy(m => m.Name)
                        .ToListAsync(cancellationToken)
                };
        }
    }
}
