﻿using Gram.Application.Abstraction;
using Gram.Application.Events.Models;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Queries
{
    public class GetEventDeleteQuery : IRequest<EventDeleteModel>
    {
        public int Id { get; set; }

        public class Handler : BaseHandler, IRequestHandler<GetEventDeleteQuery, EventDeleteModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<EventDeleteModel> Handle(GetEventDeleteQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Events
                    .Include(m => m.EventParticipations)
                    .Include(m => m.EventStatus)
                    .FirstOrDefaultAsync(m => m.Id == request.Id);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                return new EventDeleteModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    IsDeletable = !entity.EventParticipations.Any(),
                    EventName = entity.EventName,
                    EventStatus = entity.EventStatus.Title,
                    EventDescription = entity.EventDescription,
                    EventDate = entity.EventDate
                };
            }
        }
    }
}
