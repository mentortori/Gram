﻿using Gram.Application.Abstraction;
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
                var existingGuides = await DataContext.EventGuides
                    .Where(m => m.EventId == request.EventId)
                    .Select(m => m.Guide.PersonId)
                    .ToArrayAsync(cancellationToken);

                var existingAttendees = await DataContext.Attendees
                    .Where(m => m.EventId == request.EventId)
                    .Select(m => m.PersonId)
                    .ToArrayAsync(cancellationToken);

                return await DataContext.People
                    .Where(m => m.Guides.Any(n => n.IsActive))
                    .Where(m => !existingGuides.Contains(m.Id))
                    .Where(m => !existingAttendees.Contains(m.Id))
                    .Select(m => new ListItemModel
                    {
                        Id = m.Id,
                        Name = m.FirstName + " " + m.LastName
                    })
                    .OrderBy(m => m.Name)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
