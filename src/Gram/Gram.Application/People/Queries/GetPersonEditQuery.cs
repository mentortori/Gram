using Gram.Application.Abstraction;
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
        public int Id { get; set; }

        public class Handler : BaseHandler, IRequestHandler<GetPersonEditQuery, PersonEditModel>
        {
            private IMediator _mediator;

            public Handler(IDataContext dataContext, IMediator mediator) : base(dataContext)
            {
                _mediator = mediator;
            }

            public async Task<PersonEditModel> Handle(GetPersonEditQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.People.Include(m => m.Nationality).FirstOrDefaultAsync(m => m.Id == request.Id);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                return new PersonEditModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    DateOfBirth = entity.DateOfBirth,
                    NationalityId = entity.NationalityId
                };
            }
        }
    }
}
