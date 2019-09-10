using Gram.Application.Abstraction;
using Gram.Application.EventGuides.Models;
using Gram.Application.Events.Models;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Queries
{
    public class GetEventDetailQuery : IRequest<EventDetailModel>
    {
        private int Id { get; }

        public GetEventDetailQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetEventDetailQuery, EventDetailModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<EventDetailModel> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Events
                    .Include(m => m.EventStatus)
                    .Include(m => m.EventGuides)
                        .ThenInclude(m => m.Guide)
                            .ThenInclude(m => m.Person)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                return new EventDetailModel
                {
                    Id = request.Id,
                    EventName = entity.EventName,
                    EventStatus = entity.EventStatus.Title,
                    EventDescription = entity.EventDescription,
                    EventDate = entity.EventDate,
                    Guides = entity.EventGuides.Select(m => new EventGuideModel
                                {
                                    Id = m.Id,
                                    RowVersion = m.RowVersion,
                                    Name = m.Guide.Person.FirstName + " " + m.Guide.Person.LastName
                                }).OrderBy(m => m.Name)
                };
            }
        }
    }
}
