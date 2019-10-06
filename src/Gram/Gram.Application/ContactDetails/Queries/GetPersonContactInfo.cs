using Gram.Application.Abstraction;
using Gram.Application.ContactDetails.Models;
using Gram.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.ContactDetails.Queries
{
    public class GetPersonContactInfo : IRequest<ContactDetailsViewModel>
    {
        private int PersonId { get; }

        public GetPersonContactInfo(int id)
        {
            PersonId = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetPersonContactInfo, ContactDetailsViewModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<ContactDetailsViewModel> Handle(GetPersonContactInfo request, CancellationToken cancellationToken)
            {
                var entity = DataContext.PersonContactInfos
                    .Where(m => m.PersonId == request.PersonId);

                if (!entity.Any())
                    return new ContactDetailsViewModel();

                return new ContactDetailsViewModel
                {
                    Mobile = (await entity.SingleOrDefaultAsync(m => m.ContactType.Title == "Mobile", cancellationToken))?.Content,
                    Email = (await entity.SingleOrDefaultAsync(m => m.ContactType.Title == "Email", cancellationToken))?.Content,
                    Facebook = (await entity.SingleOrDefaultAsync(m => m.ContactType.Title == "Facebook", cancellationToken))?.Content,
                    Instagram = (await entity.SingleOrDefaultAsync(m => m.ContactType.Title == "Instagram", cancellationToken))?.Content,
                    Twitter = (await entity.SingleOrDefaultAsync(m => m.ContactType.Title == "Twitter", cancellationToken))?.Content,
                    Web = (await entity.SingleOrDefaultAsync(m => m.ContactType.Title == "Web", cancellationToken))?.Content
                };
            }
        }
    }
}
