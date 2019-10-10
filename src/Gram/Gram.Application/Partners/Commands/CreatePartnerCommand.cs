using Gram.Application.Abstraction;
using Gram.Application.GeneralTypes.Queries;
using Gram.Application.Interfaces;
using Gram.Application.Partners.Models;
using Gram.Domain.Entities;
using Gram.Domain.Enums;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Partners.Commands
{
    public class CreatePartnerCommand : IRequest<int>
    {
        private PartnerCreateModel Model { get; }
        private IMediator _mediator;

        public CreatePartnerCommand(PartnerCreateModel model, IMediator mediator)
        {
            Model = model;
            _mediator = mediator;
        }

        public class Handler : BaseHandler, IRequestHandler<CreatePartnerCommand, int>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<int> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
            {
                var entity = new Partner
                {
                    Name = request.Model.Name,
                    IsActive = request.Model.IsActive
                };

                var contactTypes = await request._mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeEnum.GeneralTypeParents.ContactType), cancellationToken);

                if (!string.IsNullOrWhiteSpace(request.Model.ContactDetails.Mobile))
                    entity.PartnerContactInfos.Add(new PartnerContactInfo
                    {
                        ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Mobile").Id,
                        Content = request.Model.ContactDetails.Mobile
                    });

                if (!string.IsNullOrWhiteSpace(request.Model.ContactDetails.Email))
                    entity.PartnerContactInfos.Add(new PartnerContactInfo
                    {
                        ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Email").Id,
                        Content = request.Model.ContactDetails.Email
                    });

                if (!string.IsNullOrWhiteSpace(request.Model.ContactDetails.Facebook))
                    entity.PartnerContactInfos.Add(new PartnerContactInfo
                    {
                        ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Facebook").Id,
                        Content = request.Model.ContactDetails.Facebook
                    });

                if (!string.IsNullOrWhiteSpace(request.Model.ContactDetails.Instagram))
                    entity.PartnerContactInfos.Add(new PartnerContactInfo
                    {
                        ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Instagram").Id,
                        Content = request.Model.ContactDetails.Instagram
                    });

                if (!string.IsNullOrWhiteSpace(request.Model.ContactDetails.Twitter))
                    entity.PartnerContactInfos.Add(new PartnerContactInfo
                    {
                        ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Twitter").Id,
                        Content = request.Model.ContactDetails.Twitter
                    });

                if (!string.IsNullOrWhiteSpace(request.Model.ContactDetails.Web))
                    entity.PartnerContactInfos.Add(new PartnerContactInfo
                    {
                        ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Web").Id,
                        Content = request.Model.ContactDetails.Web
                    });

                await DataContext.Partners.AddAsync(entity, cancellationToken);
                await DataContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}
