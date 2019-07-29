using Gram.Application.Events.Models;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommand : IRequest
    {
        public int Id { get; set; }

        public EventEditModel EventEditModel { get; set; }

        public class Handler : IRequestHandler<UpdateEventCommand, Unit>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
            {
                var entity = new Event
                {
                    Id = request.Id,
                    EventName = request.EventEditModel.EventName,
                    EventStatusId = request.EventEditModel.EventStatusId,
                    EventDescription = request.EventEditModel.EventDescription,
                    EventDate = request.EventEditModel.EventDate,
                    RowVersion = request.EventEditModel.RowVersion
                };

                _context.Events.Attach(entity).State = EntityState.Modified;

                var exists = await _context.Events.FindAsync(request.Id);

                if (exists == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
