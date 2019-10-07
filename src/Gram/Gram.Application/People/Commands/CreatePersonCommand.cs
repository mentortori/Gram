using Gram.Application.Abstraction;
using Gram.Application.GeneralTypes.Queries;
using Gram.Application.Interfaces;
using Gram.Application.People.Models;
using Gram.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Gram.Domain.Enums.GeneralTypeEnum;

namespace Gram.Application.People.Commands
{
    public class CreatePersonCommand : IRequest<int>
    {
        private PersonCreateModel Model { get; }
        private IMediator _mediator;

        public CreatePersonCommand(PersonCreateModel model, IMediator mediator)
        {
            Model = model;
            _mediator = mediator;
        }

        public class Handler : BaseHandler, IRequestHandler<CreatePersonCommand, int>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
            {
                var entity = new Person
                {
                    FirstName = request.Model.FirstName,
                    LastName = request.Model.LastName,
                    DateOfBirth = request.Model.DateOfBirth,
                    NationalityId = request.Model.NationalityId
                };

                var contactTypes = await request._mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.ContactType), cancellationToken);

                if (!string.IsNullOrWhiteSpace(request.Model.ContactDetails.Mobile))
                    entity.PersonContactInfos.Add(new PersonContactInfo
                    {
                        ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Mobile").Id,
                        Content = request.Model.ContactDetails.Mobile
                    });

                if (!string.IsNullOrWhiteSpace(request.Model.ContactDetails.Email))
                    entity.PersonContactInfos.Add(new PersonContactInfo
                    {
                        ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Email").Id,
                        Content = request.Model.ContactDetails.Email
                    });

                if (!string.IsNullOrWhiteSpace(request.Model.ContactDetails.Facebook))
                    entity.PersonContactInfos.Add(new PersonContactInfo
                    {
                        ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Facebook").Id,
                        Content = request.Model.ContactDetails.Facebook
                    });

                if (!string.IsNullOrWhiteSpace(request.Model.ContactDetails.Instagram))
                    entity.PersonContactInfos.Add(new PersonContactInfo
                    {
                        ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Instagram").Id,
                        Content = request.Model.ContactDetails.Instagram
                    });

                if (!string.IsNullOrWhiteSpace(request.Model.ContactDetails.Twitter))
                    entity.PersonContactInfos.Add(new PersonContactInfo
                    {
                        ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Twitter").Id,
                        Content = request.Model.ContactDetails.Twitter
                    });

                if (!string.IsNullOrWhiteSpace(request.Model.ContactDetails.Web))
                    entity.PersonContactInfos.Add(new PersonContactInfo
                    {
                        ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Web").Id,
                        Content = request.Model.ContactDetails.Web
                    });

                await DataContext.People.AddAsync(entity, cancellationToken);
                await DataContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}
