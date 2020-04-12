using Gram.Application.Abstraction;
using Gram.Application.Attendees.Models;
using Gram.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Attendees.Queries
{
    public class GetEventAttendeesQuery : IRequest<List<AttendeeListViewModel>>
    {
        private int EventId { get; }

        public GetEventAttendeesQuery(int eventId)
        {
            EventId = eventId;
        }

        public class Handler : BaseHandler, IRequestHandler<GetEventAttendeesQuery, List<AttendeeListViewModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<AttendeeListViewModel>> Handle(GetEventAttendeesQuery request, CancellationToken cancellationToken) =>
                await DataContext.Attendees
                    .Where(m => m.EventId == request.EventId)
                    .Select(m => new AttendeeListViewModel
                    {
                        Id = m.Id,
                        Attendee = m.Person.FirstName + ' ' + m.Person.LastName,
                        AttendanceStatus = m.Status.Title,
                        StatusDate = m.StatusDate,
                        Remarks = m.Remarks
                    }).ToListAsync(cancellationToken);
        }
    }
}
