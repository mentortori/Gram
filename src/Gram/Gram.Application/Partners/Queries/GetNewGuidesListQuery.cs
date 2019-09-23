using Gram.Application.Abstraction;
using Gram.Application.Interfaces;
using Gram.Application.People.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Partners.Queries
{
    public class GetNewPartnersListQuery : IRequest<List<PersonListItemModel>>
    {
        public class Handler : BaseHandler, IRequestHandler<GetNewPartnersListQuery, List<PersonListItemModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<PersonListItemModel>> Handle(GetNewPartnersListQuery request, CancellationToken cancellationToken)
            {
                var existing = await DataContext.Guides.Select(m => m.PersonId).ToArrayAsync(cancellationToken);

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
