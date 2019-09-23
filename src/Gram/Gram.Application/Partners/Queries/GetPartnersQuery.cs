using Gram.Application.Abstraction;
using Gram.Application.Interfaces;
using Gram.Application.Partners.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Partners.Queries
{
    public class GetPartnersQuery : IRequest<List<PartnerListViewModel>>
    {
        public class Handler : BaseHandler, IRequestHandler<GetPartnersQuery, List<PartnerListViewModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<PartnerListViewModel>> Handle(GetPartnersQuery request, CancellationToken cancellationToken)
            {
                return await DataContext.Guides
                    .Include(m => m.EventGuides)
                    .Include(m => m.Person)
                        .ThenInclude(m => m.Nationality)
                    .Select(m => new PartnerListViewModel
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
}
