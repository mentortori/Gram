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
    public class GetPartnerDeleteQuery : IRequest<PartnerDeleteModel>
    {
        private int Id { get; }

        public GetPartnerDeleteQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetPartnerDeleteQuery, PartnerDeleteModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<PartnerDeleteModel> Handle(GetPartnerDeleteQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Guides
                    .Include(m => m.Person)
                        .ThenInclude(m => m.Nationality)
                    .Include(m => m.EventGuides)
                        .ThenInclude(m => m.Event)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Guide), request.Id);

                return new PartnerDeleteModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    IsDeletable = !entity.EventGuides.Any(),
                    FirstName = entity.Person.FirstName,
                    LastName = entity.Person.LastName,
                    DateOfBirth = entity.Person.DateOfBirth,
                    Nationality = entity.Person.Nationality?.Title,
                    EventsCount = entity.EventGuides.Count(),
                    IsActive = entity.IsActive
                };
            }
        }
    }
}
