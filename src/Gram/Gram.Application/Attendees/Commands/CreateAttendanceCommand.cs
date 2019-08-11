using Gram.Application.Abstraction;
using Gram.Application.Attendees.Models;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Attendees.Commands
{
    public class CreateAttendanceCommand : IRequest
    {
        private int EventId { get; }
        private AttendanceCreateModel Model { get; }

        public CreateAttendanceCommand(int eventId, AttendanceCreateModel model)
        {
            EventId = eventId;
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<CreateAttendanceCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(CreateAttendanceCommand request, CancellationToken cancellationToken)
            {
                var entity = new Attendance
                {
                    EventId = request.EventId,
                    PersonId = request.Model.PersonId,
                    StatusId = request.Model.StatusId,
                    StatusDate = request.Model.StatusDate,
                    Remarks = request.Model.Remarks
                };

                await DataContext.Attendees.AddAsync(entity, cancellationToken);
                await DataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
