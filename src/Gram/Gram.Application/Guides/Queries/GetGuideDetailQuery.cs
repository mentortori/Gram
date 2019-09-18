using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Guides.Models;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Guides.Queries
{
    public class GetGuideDetailQuery : IRequest<GuideDetailModel>
    {
        private int Id { get; }

        public GetGuideDetailQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetGuideDetailQuery, GuideDetailModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<GuideDetailModel> Handle(GetGuideDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Guides
                    .Include(m => m.Person)
                        .ThenInclude(m => m.Nationality)
                    .Include(m => m.EventGuides)
                        .ThenInclude(m => m.Event)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Guide), request.Id);

                return new GuideDetailModel
                {
                    Id = request.Id,
                    Name = entity.Person.FirstName + " " + entity.Person.LastName,
                    DateOfBirth = entity.Person.DateOfBirth,
                    Nationality = entity.Person.Nationality?.Title,
                    Events = entity.EventGuides.Select(m => new GuideDetailModel.GuideEventModel
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
