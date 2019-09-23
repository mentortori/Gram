using Gram.Application.Abstraction;
using Gram.Application.Interfaces;
using Gram.Application.People.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.EventGuides.Queries
{
    public class GetNewEventGuidesListQuery : IRequest<List<PersonListItemModel>>
    {
        private int EventId { get; }

        public GetNewEventGuidesListQuery(int parentId)
        {
            EventId = parentId;
        }

        public class Handler : BaseHandler, IRequestHandler<GetNewEventGuidesListQuery, List<PersonListItemModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<PersonListItemModel>> Handle(GetNewEventGuidesListQuery request, CancellationToken cancellationToken)
            {
                var existing = await DataContext.EventGuides.Where(m => m.EventId == request.EventId)
                    .Select(m => m.GuideId)
                    .ToArrayAsync(cancellationToken);

                return await DataContext.Guides
                    .Where(m => m.IsActive)
                    .Include(m => m.Person)
                    .Where(m => !existing.Contains(m.Id))
                    .Select(m => new PersonListItemModel
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
