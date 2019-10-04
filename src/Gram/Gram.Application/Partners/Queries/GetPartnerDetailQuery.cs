using Gram.Application.Abstraction;
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
    public class GetPartnerDetailQuery : IRequest<PartnerDetailModel>
    {
        private int Id { get; }

        public GetPartnerDetailQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetPartnerDetailQuery, PartnerDetailModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<PartnerDetailModel> Handle(GetPartnerDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Partners
                    .Include(m => m.EventPartners)
                        .ThenInclude(m => m.Event)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Partner), request.Id);

                return new PartnerDetailModel
                {
                    Id = request.Id,
                    Name = entity.Name,
                    Events = entity.EventPartners.Select(m => new PartnerDetailModel.PartnerEventModel
                    {
                        Id = m.EventId,
                        Name = m.Event.EventName
                    }),
                    IsActive = entity.IsActive
                };
            }
        }
    }
}
