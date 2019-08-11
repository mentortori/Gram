using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Application.People.Models;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.People.Queries
{
    public class GetPersonDeleteQuery : IRequest<PersonDeleteModel>
    {
        private int Id { get; }

        public GetPersonDeleteQuery(int id)
        {
            Id = id;
        }
        
        public class Handler : BaseHandler, IRequestHandler<GetPersonDeleteQuery, PersonDeleteModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<PersonDeleteModel> Handle(GetPersonDeleteQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.People
                    .Include(m => m.Attendees)
                    .Include(m => m.Employees)
                    .Include(m => m.Nationality)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Person), request.Id);

                return new PersonDeleteModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    IsDeletable = !entity.Attendees.Any() && !entity.Employees.Any(),
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    DateOfBirth = entity.DateOfBirth,
                    Nationality = entity.Nationality?.Title
                };
            }
        }
    }
}
