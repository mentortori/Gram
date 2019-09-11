using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Guides.Models;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Guides.Queries
{
    public class GetGuideDeleteQuery : IRequest<GuideDeleteModel>
    {
        private int Id { get; }

        public GetGuideDeleteQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetGuideDeleteQuery, GuideDeleteModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<GuideDeleteModel> Handle(GetGuideDeleteQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Guides
                    .Include(m => m.Person)
                        .ThenInclude(m => m.Nationality)
                    .Include(m => m.EventGuides)
                        .ThenInclude(m => m.Event)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Guide), request.Id);

                return new GuideDeleteModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    IsDeletable = !entity.EventGuides.Any(),
                    FirstName = entity.Person.FirstName,
                    LastName = entity.Person.LastName,
                    DateOfBirth = entity.Person.DateOfBirth,
                    Nationality = entity.Person.Nationality?.Title,
                    EventsCount = entity.EventGuides.Count(),
                    IsActive = entity.IsActive
                };
            }
        }
    }
}
