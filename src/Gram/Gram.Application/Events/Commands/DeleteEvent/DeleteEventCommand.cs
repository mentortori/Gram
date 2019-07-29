using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteEventCommand, Unit>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Events.FindAsync(request.Id);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                _context.Events.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
