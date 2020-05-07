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

            public async Task<List<PartnerListViewModel>> Handle(GetPartnersQuery request, CancellationToken cancellationToken) =>
                await DataContext.Partners
                    .Select(m => new PartnerListViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        EventsCount = m.EventPartners.Count(),
                        IsActive = m.IsActive
                    }).ToListAsync(cancellationToken);
        }
    }
}
