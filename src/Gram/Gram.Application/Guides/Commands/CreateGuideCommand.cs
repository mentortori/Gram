using Gram.Application.Abstraction;
using Gram.Application.Guides.Models;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Guides.Commands
{
    public class CreateGuideCommand : IRequest<int>
    {
        private GuideCreateModel Model { get; }

        public CreateGuideCommand(GuideCreateModel model)
        {
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<CreateGuideCommand, int>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<int> Handle(CreateGuideCommand request, CancellationToken cancellationToken)
            {
                var entity = new Guide
                {
                    PersonId = request.Model.PersonId,
                    IsActive = request.Model.IsActive
                };

                await DataContext.Guides.AddAsync(entity, cancellationToken);
                await DataContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}
