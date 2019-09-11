using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Guides.Models;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Guides.Commands
{
    public class UpdateGuideCommand : IRequest
    {
        private GuideEditModel Model { get; }

        public UpdateGuideCommand(GuideEditModel model)
        {
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<UpdateGuideCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(UpdateGuideCommand request, CancellationToken cancellationToken)
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
