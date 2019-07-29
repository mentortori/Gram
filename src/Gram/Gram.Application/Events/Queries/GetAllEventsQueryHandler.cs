using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gram.Application.Events.Models;
using Gram.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gram.Application.Events.Queries
{
    public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, List<EventsListViewModel>>
    {
        private readonly IDataContext _context;

        public GetAllEventsQueryHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<List<EventsListViewModel>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Events.Select(m => new EventsListViewModel
            {
                Id = m.Id,
                EventName = m.EventName,
                EventStatus = m.EventStatus.Title,
                EventDate = m.EventDate
            }).ToListAsync(cancellationToken);
        }
    }
}
