using Gram.Application.Abstraction;
using Gram.Application.Attendees.Models;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Attendees.Commands
{
    public class UpdateAttendanceCommand : IRequest
    {
        private AttendanceEditModel Model { get; }

        public UpdateAttendanceCommand(AttendanceEditModel model)
        {
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<UpdateAttendanceCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
            {
                if ((await DataContext.People.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Model.Id, cancellationToken)) == null)
                    throw new EntityNotFoundException(nameof(Event), request.Model.Id);

                var entity = new Attendance()
                {
                    Id = request.Model.Id,
                    RowVersion = request.Model.RowVersion,
                    EventId = request.Model.EventId,
                    PersonId = request.Model.PersonId,
                    StatusId = request.Model.StatusId,
                    StatusDate = request.Model.StatusDate,
                    Remarks = request.Model.Remarks
                };

                try
                {
                    DataContext.Attendees.Attach(entity).State = EntityState.Modified;
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
