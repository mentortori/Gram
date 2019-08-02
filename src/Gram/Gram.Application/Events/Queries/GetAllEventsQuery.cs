using Gram.Application.Abstraction;
using Gram.Application.Events.Models;
using Gram.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Queries
{
    public class GetAllEventsQuery : IRequest<List<EventsListViewModel>>
    {
        public class Handler : BaseHandler, IRequestHandler<GetAllEventsQuery, List<EventsListViewModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<EventsListViewModel>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
            {
                return await DataContext.Events.Select(m => new EventsListViewModel
                {
                    Id = m.Id,
                    EventName = m.EventName,
                    EventStatus = m.EventStatus.Title,
                    EventDate = m.EventDate
                }).ToListAsync(cancellationToken);
            }
        }
    }
}
