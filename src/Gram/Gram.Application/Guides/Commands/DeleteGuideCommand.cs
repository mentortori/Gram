using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Guides.Commands
{
    public class DeleteGuideCommand : IRequest
    {
        private int Id { get; }
        private byte[] RowVersion { get; }

        public DeleteGuideCommand(int id, byte[] rowVersion)
        {
            Id = id;
            RowVersion = rowVersion;
        }

        public class Handler : BaseHandler, IRequestHandler<DeleteGuideCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(DeleteGuideCommand request, CancellationToken cancellationToken)
            {
                if ((await DataContext.Guides.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken)) == null)
                    throw new EntityNotFoundException(nameof(Guide), request.Id);

                var entity = new Guide
                {
                    Id = request.Id,
                    RowVersion = request.RowVersion
                };

                try
                {
                    DataContext.Guides.Remove(entity);
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
