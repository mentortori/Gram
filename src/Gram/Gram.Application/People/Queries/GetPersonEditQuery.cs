using Gram.Application.Abstraction;
using Gram.Application.ContactDetails.Queries;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Application.People.Models;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.People.Queries
{
    public class GetPersonEditQuery : IRequest<PersonEditModel>
    {
        private int Id { get; }
        private IMediator _mediator;

        public GetPersonEditQuery(int id, IMediator mediator)
        {
            Id = id;
            _mediator = mediator;
        }

        public class Handler : BaseHandler, IRequestHandler<GetPersonEditQuery, PersonEditModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<PersonEditModel> Handle(GetPersonEditQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.People
                    .Include(m => m.Nationality)
                    .Include(m => m.PersonContactInfos)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Person), request.Id);

                return new PersonEditModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    DateOfBirth = entity.DateOfBirth,
                    NationalityId = entity.NationalityId,
                    ContactDetails = await request._mediator.Send(new GetPersonContactInfoEditQuery(request.Id, request._mediator), cancellationToken)
                };
            }
        }
    }
}
