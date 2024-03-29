﻿using Gram.Application.Abstraction;
using Gram.Application.ContactDetails.Models;
using Gram.Application.GeneralTypes.Queries;
using Gram.Application.Interfaces;
using Gram.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.ContactDetails.Queries
{
    public class GetPersonContactInfoEditQuery : IRequest<ContactDetailsUpdateModel>
    {
        private int PersonId { get; }
        private IMediator _mediator;

        public GetPersonContactInfoEditQuery(int id, IMediator mediator)
        {
            PersonId = id;
            _mediator = mediator;
        }

        public class Handler : BaseHandler, IRequestHandler<GetPersonContactInfoEditQuery, ContactDetailsUpdateModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<ContactDetailsUpdateModel> Handle(GetPersonContactInfoEditQuery request, CancellationToken cancellationToken)
            {
                var entity = DataContext.PersonContactInfos
                    .Include(m => m.ContactType)
                    .Where(m => m.PersonId == request.PersonId);

                if (!entity.Any())
                    return new ContactDetailsUpdateModel();

                var result = new ContactDetailsUpdateModel();
                var mobile = await entity.FirstOrDefaultAsync(m => m.ContactType.Title == "Mobile", cancellationToken);
                var email = await entity.FirstOrDefaultAsync(m => m.ContactType.Title == "Email", cancellationToken);
                var facebook = await entity.FirstOrDefaultAsync(m => m.ContactType.Title == "Facebook", cancellationToken);
                var instagram = await entity.FirstOrDefaultAsync(m => m.ContactType.Title == "Instagram", cancellationToken);
                var twitter = await entity.FirstOrDefaultAsync(m => m.ContactType.Title == "Twitter", cancellationToken);
                var web = await entity.FirstOrDefaultAsync(m => m.ContactType.Title == "Web", cancellationToken);


                if (mobile != null)
                {
                    result.MobileId = mobile.Id;
                    result.Mobile = mobile.Content;
                    result.MobileRowVersion = mobile.RowVersion;
                }

                if (email != null)
                {
                    result.EmailId = email.Id;
                    result.Email = email.Content;
                    result.EmailRowVersion = email.RowVersion;
                }

                if (facebook != null)
                {
                    result.FacebookId = facebook.Id;
                    result.Facebook = facebook.Content;
                    result.FacebookRowVersion = facebook.RowVersion;
                }

                if (instagram != null)
                {
                    result.InstagramId = instagram.Id;
                    result.Instagram = instagram.Content;
                    result.InstagramRowVersion = instagram.RowVersion;
                }

                if (twitter != null)
                {
                    result.TwitterId = twitter.Id;
                    result.Twitter = twitter.Content;
                    result.TwitterRowVersion = twitter.RowVersion;
                }

                if (web != null)
                {
                    result.WebId = web.Id;
                    result.Web = web.Content;
                    result.WebRowVersion = web.RowVersion;
                }

                return result;
            }
        }
    }
}
