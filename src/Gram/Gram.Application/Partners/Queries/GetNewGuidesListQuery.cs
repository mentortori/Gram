using Gram.Application.Abstraction;
using Gram.Application.Interfaces;
using Gram.Application.SharedModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Partners.Queries
{
    public class GetNewPartnersListQuery : IRequest<List<ListItemModel>>
    {
        public class Handler : BaseHandler, IRequestHandler<GetNewPartnersListQuery, List<ListItemModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<ListItemModel>> Handle(GetNewPartnersListQuery request, CancellationToken cancellationToken)
            {
                var existing = await DataContext.Guides.Select(m => m.PersonId).ToArrayAsync(cancellationToken);

                return await DataContext.People
                    .Where(m => !existing.Contains(m.Id))
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
