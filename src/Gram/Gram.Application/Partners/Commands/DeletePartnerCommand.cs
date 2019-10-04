using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Partners.Commands
{
    public class DeletePartnerCommand : IRequest
    {
        private int Id { get; }
        private byte[] RowVersion { get; }

        public DeletePartnerCommand(int id, byte[] rowVersion)
        {
            Id = id;
            RowVersion = rowVersion;
        }

        public class Handler : BaseHandler, IRequestHandler<DeletePartnerCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
            {
                if ((await DataContext.Partners.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken)) == null)
                    throw new EntityNotFoundException(nameof(Partner), request.Id);

                var entity = new Partner
                {
                    Id = request.Id,
                    RowVersion = request.RowVersion
                };

                try
                {
                    DataContext.Partners.Remove(entity);
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
