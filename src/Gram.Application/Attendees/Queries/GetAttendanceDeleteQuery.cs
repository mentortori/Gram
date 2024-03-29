﻿using Gram.Application.Abstraction;
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
    public class GetAttendanceDeleteQuery : IRequest<DeleteViewModel>
    {
        private int Id { get; }

        public GetAttendanceDeleteQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetAttendanceDeleteQuery, DeleteViewModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<DeleteViewModel> Handle(GetAttendanceDeleteQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Attendees
                    .Include(m => m.Person)
                    .Include(m => m.Status)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Attendance), request.Id);

                return new DeleteViewModel
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
