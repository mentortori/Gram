﻿using Gram.Application.Events.Models;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Queries
{
    public class GetEventEditQuery : IRequest<EventEditModel>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetEventEditQuery, EventEditModel>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<EventEditModel> Handle(GetEventEditQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Events.Include(m => m.EventStatus).FirstOrDefaultAsync(m => m.Id == request.Id);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                return new EventEditModel
                {
                    Id = request.Id,
                    EventName = entity.EventName,
                    EventStatusId = entity.EventStatusId,
                    EventDescription = entity.EventDescription,
                    EventDate = entity.EventDate,
                    RowVersion = entity.RowVersion
                };
            }
        }
    }
}