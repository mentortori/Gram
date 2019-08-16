using Gram.Application.Abstraction;
using Gram.Application.Interfaces;
using Gram.Application.People.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Attendees.Queries
{
    public class GetNewAttendeesListQuery : IRequest<List<PersonListItemModel>>
    {
        private int EventId { get; }

        public GetNewAttendeesListQuery(int parentId)
        {
            EventId = parentId;
        }

        public class Handler : BaseHandler, IRequestHandler<GetNewAttendeesListQuery, List<PersonListItemModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<PersonListItemModel>> Handle(GetNewAttendeesListQuery request, CancellationToken cancellationToken)
            {
                var existing = await DataContext.Attendees.Where(m => m.EventId == request.EventId)
                    .Select(m => m.PersonId)
                    .ToArrayAsync(cancellationToken);

                return await DataContext.People
                    .Where(m => !existing.Contains(m.Id))
                    .Select(m => new PersonListItemModel
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
