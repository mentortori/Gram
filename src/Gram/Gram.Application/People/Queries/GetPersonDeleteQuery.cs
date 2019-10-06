using Gram.Application.Abstraction;
using Gram.Application.ContactDetails.Queries;
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
        private IMediator _mediator;

        public GetPersonDeleteQuery(int id, IMediator mediator)
        {
            Id = id;
            _mediator = mediator;
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
                    .Include(m => m.Guides)
                    .Include(m => m.Nationality)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Person), request.Id);

                return new PersonDeleteModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    IsDeletable = !entity.Attendees.Any() && !entity.Employees.Any() && !entity.Guides.Any(),
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    DateOfBirth = entity.DateOfBirth,
                    Nationality = entity.Nationality?.Title,
                    ContactDetails = await request._mediator.Send(new GetPersonContactInfo(request.Id)),
                };
            }
        }
    }
}
