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
    public class GetEventDetailQuery : IRequest<EventDetailModel>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetEventDetailQuery, EventDetailModel>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<EventDetailModel> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Events.Include(m => m.EventStatus).FirstOrDefaultAsync(m => m.Id == request.Id);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                return new EventDetailModel
                {
                    Id = request.Id,
                    EventName = entity.EventName,
                    EventStatus = entity.EventStatus.Title,
                    EventDescription = entity.EventDescription,
                    EventDate = entity.EventDate
                };
            }
        }
    }
}
