using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.People.Commands
{
    public class DeletePersonCommand : IRequest
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }

        public class Handler : BaseHandler, IRequestHandler<DeletePersonCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
            {
                if ((await DataContext.People.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id)) == null)
                    throw new EntityNotFoundException(nameof(Person), request.Id);

                var entity = new Person
                {
                    Id = request.Id,
                    RowVersion = request.RowVersion
                };

                try
                {
                    DataContext.People.Remove(entity);
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
