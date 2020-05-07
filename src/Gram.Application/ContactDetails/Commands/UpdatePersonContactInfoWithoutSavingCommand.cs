using Gram.Application.Abstraction;
using Gram.Application.ContactDetails.Models;
using Gram.Application.GeneralTypes.Queries;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using Gram.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.ContactDetails.Commands
{
    public class UpdatePersonContactInfoWithoutSavingCommand : IRequest
    {
        public int PersonId { get; }
        public ContactDetailsUpdateModel Model { get; }
        private IMediator _mediator;

        public UpdatePersonContactInfoWithoutSavingCommand(int personId, ContactDetailsUpdateModel model, IMediator mediator)
        {
            PersonId = personId;
            Model = model;
            _mediator = mediator;
        }

        public class Handler : BaseHandler, IRequestHandler<UpdatePersonContactInfoWithoutSavingCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(UpdatePersonContactInfoWithoutSavingCommand request, CancellationToken cancellationToken)
            {
                var contactTypes = await request._mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.ContactType), cancellationToken);

                foreach (var item in contactTypes)
                {
                    var personContactInfo = new PersonContactInfo();

                    switch (item.Title)
                    {
                        case "Mobile":
                            personContactInfo = new PersonContactInfo
                            {
                                Id = request.Model.MobileId,
                                PersonId = request.PersonId,
                                ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Mobile").Id,
                                Content = request.Model.Mobile,
                                RowVersion = request.Model.MobileRowVersion
                            };
                            break;
                        case "Email":
                            personContactInfo = new PersonContactInfo
                            {
                                Id = request.Model.EmailId,
                                PersonId = request.PersonId,
                                ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Email").Id,
                                Content = request.Model.Email,
                                RowVersion = request.Model.EmailRowVersion
                            };
                            break;
                        case "Facebook":
                            personContactInfo = new PersonContactInfo
                            {
                                Id = request.Model.FacebookId,
                                PersonId = request.PersonId,
                                ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Facebook").Id,
                                Content = request.Model.Facebook,
                                RowVersion = request.Model.FacebookRowVersion
                            };
                            break;
                        case "Instagram":
                            personContactInfo = new PersonContactInfo
                            {
                                Id = request.Model.InstagramId,
                                PersonId = request.PersonId,
                                ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Instagram").Id,
                                Content = request.Model.Instagram,
                                RowVersion = request.Model.InstagramRowVersion
                            };
                            break;
                        case "Twitter":
                            personContactInfo = new PersonContactInfo
                            {
                                Id = request.Model.TwitterId,
                                PersonId = request.PersonId,
                                ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Twitter").Id,
                                Content = request.Model.Twitter,
                                RowVersion = request.Model.TwitterRowVersion
                            };
                            break;
                        case "Web":
                            personContactInfo = new PersonContactInfo
                            {
                                Id = request.Model.WebId,
                                PersonId = request.PersonId,
                                ContactTypeId = contactTypes.SingleOrDefault(m => m.Title == "Web").Id,
                                Content = request.Model.Web,
                                RowVersion = request.Model.WebRowVersion
                            };
                            break;
                    }

                    try
                    {
                        if (personContactInfo.Id == 0 && !string.IsNullOrWhiteSpace(personContactInfo.Content))
                            DataContext.PersonContactInfos.Add(personContactInfo);
                        else if (personContactInfo.Id != 0 && string.IsNullOrWhiteSpace(personContactInfo.Content))
                            DataContext.PersonContactInfos.Attach(personContactInfo).State = EntityState.Deleted;
                        else if (personContactInfo.Id != 0 && !string.IsNullOrWhiteSpace(personContactInfo.Content))
                            DataContext.PersonContactInfos.Attach(personContactInfo).State = EntityState.Modified;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        throw ex;
                    }
                }

                return Unit.Value;
            }
        }
    }
}
