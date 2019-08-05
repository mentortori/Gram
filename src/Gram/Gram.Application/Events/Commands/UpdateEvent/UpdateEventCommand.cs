using Gram.Application.Abstraction;
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

        public class Handler : BaseHandler, IRequestHandler<UpdateEventCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
            {
                if ((await DataContext.Events.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id)) == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                var entity = new Event
                {
                    Id = request.Id,
                    EventName = request.EventEditModel.EventName,
                    EventStatusId = request.EventEditModel.EventStatusId,
                    EventDescription = request.EventEditModel.EventDescription,
                    EventDate = request.EventEditModel.EventDate,
                    RowVersion = request.EventEditModel.RowVersion
                };

                DataContext.Events.Attach(entity).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
