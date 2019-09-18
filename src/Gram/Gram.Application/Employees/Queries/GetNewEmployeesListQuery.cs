using Gram.Application.Abstraction;
using Gram.Application.Interfaces;
using Gram.Application.People.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Employees.Queries
{
    public class GetNewEmployeesListQuery : IRequest<List<PersonListItemModel>>
    {
        public class Handler : BaseHandler, IRequestHandler<GetNewEmployeesListQuery, List<PersonListItemModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<PersonListItemModel>> Handle(GetNewEmployeesListQuery request, CancellationToken cancellationToken)
            {
                var existing = await DataContext.Employees.Select(m => m.PersonId).ToArrayAsync(cancellationToken);

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
