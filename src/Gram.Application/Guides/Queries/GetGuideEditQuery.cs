using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Guides.Models;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Guides.Queries
{
    public class GetGuideEditQuery : IRequest<GuideEditModel>
    {
        private int Id { get; }

        public GetGuideEditQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetGuideEditQuery, GuideEditModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<GuideEditModel> Handle(GetGuideEditQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Guides
                    .Include(m => m.Person)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Guide), request.Id);

                return new GuideEditModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    PersonId = entity.PersonId,
                    Name = entity.Person.FirstName + " " + entity.Person.LastName,
                    IsActive = entity.IsActive
                };
            }
        }
    }
}
