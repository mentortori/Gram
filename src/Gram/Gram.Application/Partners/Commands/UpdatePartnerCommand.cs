using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Application.Partners.Models;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Partners.Commands
{
    public class UpdatePartnerCommand : IRequest
    {
        private PartnerEditModel Model { get; }

        public UpdatePartnerCommand(PartnerEditModel model)
        {
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<UpdatePartnerCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
            {
                if ((await DataContext.Guides.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Model.Id, cancellationToken)) == null)
                    throw new EntityNotFoundException(nameof(Guide), request.Model.Id);

                var entity = new Guide
                {
                    Id = request.Model.Id,
                    RowVersion = request.Model.RowVersion,
                    PersonId = request.Model.PersonId,
                    IsActive = request.Model.IsActive
                };

                try
                {
                    DataContext.Guides.Attach(entity).State = EntityState.Modified;
                    await DataContext.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }
            }
        }
    }
}
