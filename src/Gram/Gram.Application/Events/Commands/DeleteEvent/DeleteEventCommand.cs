using Gram.Application.Abstraction;
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

        public class Handler : BaseHandler, IRequestHandler<DeleteEventCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Events.FindAsync(request.Id);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Event), request.Id);

                DataContext.Events.Remove(entity);
                await DataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
