using Gram.Application.Abstraction;
using Gram.Application.Events.Models;
using Gram.Application.Exceptions;
using Gram.Application.GeneralTypes.Queries;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using static Gram.Domain.Enums.GeneralTypeEnum;

namespace Gram.Application.Events.Queries
{
    public class GetEventEditQuery : IRequest<EventEditModel>
    {
        public int Id { get; set; }

        public class Handler : BaseHandler, IRequestHandler<GetEventEditQuery, EventEditModel>
        {
            private IMediator _mediator;

            public Handler(IDataContext dataContext, IMediator mediator) : base(dataContext)
            {
                _mediator = mediator;
            }

            public async Task<EventEditModel> Handle(GetEventEditQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Events.Include(m => m.EventStatus).FirstOrDefaultAsync(m => m.Id == request.Id);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                return new EventEditModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    Statuses = await _mediator.Send(new GetDropDownListQuery((int)GeneralTypeParents.EventStatus)),
                    EventName = entity.EventName,
                    EventStatusId = entity.EventStatusId,
                    EventDescription = entity.EventDescription,
                    EventDate = entity.EventDate
                };
            }
        }
    }
}
