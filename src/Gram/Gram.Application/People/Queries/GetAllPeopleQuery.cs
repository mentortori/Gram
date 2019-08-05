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
    public class GetAllPeopleQuery : IRequest<List<PersonListViewModel>>
    {
        public class Handler : BaseHandler, IRequestHandler<GetAllPeopleQuery, List<PersonListViewModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<PersonListViewModel>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
            {
                return await DataContext.People.Select(m => new PersonListViewModel
                {
                    Id = m.Id,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    DateOfBirth = m.DateOfBirth,
                    Nationality = m.Nationality.Title
                }).ToListAsync(cancellationToken);
            }
        }
    }
}
