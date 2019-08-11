using Gram.Application.Abstraction;
using Gram.Application.Interfaces;
using Gram.Application.People.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.People.Queries
{
    public class GetPeopleDropDownListQuery : IRequest<List<PersonDropDownItemModel>>
    {
        private int EventId { get; }

        public GetPeopleDropDownListQuery(int parentId)
        {
            EventId = parentId;
        }

        public class Handler : BaseHandler, IRequestHandler<GetPeopleDropDownListQuery, List<PersonDropDownItemModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<PersonDropDownItemModel>> Handle(GetPeopleDropDownListQuery request, CancellationToken cancellationToken)
                => await DataContext.People
                    .Include(m => m.Attendees)
                    //.Where(m => m.Attendees.Any(p => p.EventId == request.EventId))
                    .Select(m => new PersonDropDownItemModel
                    {
                        Id = m.Id,
                        Name = m.FirstName + " " + m.LastName
                    })
                    .OrderBy(m => m.Name)
                    .ToListAsync(cancellationToken);
        }
    }
}
