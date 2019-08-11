using Gram.Application.Abstraction;
using Gram.Application.Attendees.Models;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Attendees.Queries
{
    public class GetAttendanceDeleteQuery : IRequest<AttendanceDeleteModel>
    {
        private int Id { get; }

        public GetAttendanceDeleteQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetAttendanceDeleteQuery, AttendanceDeleteModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<AttendanceDeleteModel> Handle(GetAttendanceDeleteQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Attendees
                    .Include(m => m.Person)
                    .Include(m => m.Status)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                return new AttendanceDeleteModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    EventId = entity.EventId,
                    Attendee = entity.Person.FirstName + " " + entity.Person.LastName,
                    Status = entity.Status.Title,
                    StatusDate = entity.StatusDate,
                    Remarks = entity.Remarks
                };
            }
        }
    }
}
