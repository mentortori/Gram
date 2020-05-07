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
    public class GetAttendanceEditQuery : IRequest<UpdateDto>
    {
        private int Id { get; }

        public GetAttendanceEditQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetAttendanceEditQuery, UpdateDto>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<UpdateDto> Handle(GetAttendanceEditQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Attendees
                    .Include(m => m.Person)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Attendance), request.Id);

                return new UpdateDto
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    EventId = entity.EventId,
                    PersonId = entity.PersonId,
                    Attendee = entity.Person.FirstName + " " + entity.Person.LastName,
                    StatusId = entity.StatusId,
                    StatusDate = entity.StatusDate,
                    Remarks = entity.Remarks
                };
            }
        }
    }
}
