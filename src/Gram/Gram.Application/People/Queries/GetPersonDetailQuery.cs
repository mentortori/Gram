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
    public class GetPersonDetailQuery : IRequest<PersonDetailModel>
    {
        private int Id { get; }

        public GetPersonDetailQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetPersonDetailQuery, PersonDetailModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<PersonDetailModel> Handle(GetPersonDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.People
                    .Include(m => m.Nationality)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Person), request.Id);

                return new PersonDetailModel
                {
                    Id = request.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    DateOfBirth = entity.DateOfBirth,
                    Nationality = entity.NationalityId.HasValue ? entity.Nationality.Title : ""
                };
            }
        }
    }
}
