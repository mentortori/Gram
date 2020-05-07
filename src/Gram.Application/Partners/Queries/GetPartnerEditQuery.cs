using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Application.Partners.Models;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Partners.Queries
{
    public class GetPartnerEditQuery : IRequest<PartnerEditModel>
    {
        private int Id { get; }

        public GetPartnerEditQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetPartnerEditQuery, PartnerEditModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<PartnerEditModel> Handle(GetPartnerEditQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Partners
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Partner), request.Id);

                return new PartnerEditModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    Name = entity.Name,
                    IsActive = entity.IsActive
                };
            }
        }
    }
}
