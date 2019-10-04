using Gram.Application.Abstraction;
using Gram.Application.Interfaces;
using Gram.Application.Partners.Models;
using Gram.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Partners.Commands
{
    public class CreatePartnerCommand : IRequest<int>
    {
        private PartnerCreateModel Model { get; }

        public CreatePartnerCommand(PartnerCreateModel model)
        {
            Model = model;
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

                await DataContext.Partners.AddAsync(entity, cancellationToken);
                await DataContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}
