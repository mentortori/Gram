using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Attendees.Commands
{
    public class DeleteAttendanceCommand : IRequest
    {
        private int Id { get; }
        private byte[] RowVersion { get; }

        public DeleteAttendanceCommand(int id, byte[] rowVersion)
        {
            Id = id;
            RowVersion = rowVersion;
        }

        public class Handler : BaseHandler, IRequestHandler<DeleteAttendanceCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(DeleteAttendanceCommand request, CancellationToken cancellationToken)
            {
                if (await DataContext.Attendees.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken) == null)
                    throw new EntityNotFoundException(nameof(Attendance), request.Id);

                var entity = new Attendance
                {
                    Id = request.Id,
                    RowVersion = request.RowVersion
                };

                try
                {
                    DataContext.Attendees.Remove(entity);
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
