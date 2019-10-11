using Gram.Application.Abstraction;
using Gram.Application.ContactDetails.Queries;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Application.Partners.Models;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Partners.Queries
{
    public class GetPartnerDeleteQuery : IRequest<PartnerDeleteModel>
    {
        private int Id { get; }
        private IMediator _mediator;

        public GetPartnerDeleteQuery(int id, IMediator mediator)
        {
            Id = id;
            _mediator = mediator;
        }

        public class Handler : BaseHandler, IRequestHandler<GetPartnerDeleteQuery, PartnerDeleteModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<PartnerDeleteModel> Handle(GetPartnerDeleteQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Partners
                    .Include(m => m.EventPartners)
                        .ThenInclude(m => m.Event)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Partner), request.Id);

                return new PartnerDeleteModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    IsDeletable = !entity.EventPartners.Any(),
                    Name = entity.Name,
                    ContactDetails = await request._mediator.Send(new GetPartnerContactInfoDetailQuery(request.Id), cancellationToken),
                    EventsCount = entity.EventPartners.Count(),
                    IsActive = entity.IsActive
                };
            }
        }
    }
}
