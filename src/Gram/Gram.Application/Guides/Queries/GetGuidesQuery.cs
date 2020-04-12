using Gram.Application.Abstraction;
using Gram.Application.Guides.Models;
using Gram.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Guides.Queries
{
    public class GetGuidesQuery : IRequest<List<GuideListViewModel>>
    {
        public class Handler : BaseHandler, IRequestHandler<GetGuidesQuery, List<GuideListViewModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<GuideListViewModel>> Handle(GetGuidesQuery request, CancellationToken cancellationToken) =>
                await DataContext.Guides
                    .Select(m => new GuideListViewModel
                    {
                        Id = m.Id,
                        FirstName = m.Person.FirstName,
                        LastName = m.Person.LastName,
                        DateOfBirth = m.Person.DateOfBirth,
                        Nationality = m.Person.Nationality.Title,
                        EventsCount = m.EventGuides.Count(),
                        IsActive = m.IsActive
                    }).ToListAsync(cancellationToken);
        }
    }
}
