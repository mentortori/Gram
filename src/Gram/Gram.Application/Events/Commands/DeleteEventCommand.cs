using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Commands
{
    public class DeleteEventCommand : IRequest
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }

        public class Handler : BaseHandler, IRequestHandler<DeleteEventCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
            {
                if ((await DataContext.Events.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id)) == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                var entity = new Event
                {
                    Id = request.Id,
                    RowVersion = request.RowVersion
                };

                try
                {
                    DataContext.Events.Remove(entity);
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
