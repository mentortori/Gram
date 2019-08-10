using Gram.Application.Abstraction;
using Gram.Application.Events.Models;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Queries
{
    public class GetEventEditQuery : IRequest<EventEditModel>
    {
        private int Id { get; }

        public GetEventEditQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetEventEditQuery, EventEditModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<EventEditModel> Handle(GetEventEditQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Events
                    .Include(m => m.EventStatus)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                return new EventEditModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    EventName = entity.EventName,
                    EventStatusId = entity.EventStatusId,
                    EventDescription = entity.EventDescription,
                    EventDate = entity.EventDate
                };
            }
        }
    }
}
