using Gram.Application.Abstraction;
using Gram.Application.Events.Models;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Commands
{
    public class UpdateEventCommand : IRequest
    {
        private int Id { get; }
        private EventEditModel Model { get; }

        public UpdateEventCommand(int id, EventEditModel model)
        {
            Id = id;
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<UpdateEventCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
            {
                if ((await DataContext.Events.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken)) == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                var entity = new Event
                {
                    Id = request.Id,
                    RowVersion = request.Model.RowVersion,
                    EventName = request.Model.EventName,
                    EventStatusId = request.Model.EventStatusId,
                    EventDescription = request.Model.EventDescription,
                    EventDate = request.Model.EventDate
                };

                try
                {
                    DataContext.Events.Attach(entity).State = EntityState.Modified;
                    await DataContext.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }
            }
        }
    }
}
