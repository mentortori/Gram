using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Employees.Commands
{
    public class DeleteEmployeeCommand : IRequest
    {
        private int Id { get; }
        private byte[] RowVersion { get; }

        public DeleteEmployeeCommand(int id, byte[] rowVersion)
        {
            Id = id;
            RowVersion = rowVersion;
        }

        public class Handler : BaseHandler, IRequestHandler<DeleteEmployeeCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
            {
                if ((await DataContext.Employees.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken)) == null)
                    throw new EntityNotFoundException(nameof(Employees), request.Id);

                var entity = new Employee
                {
                    Id = request.Id,
                    RowVersion = request.RowVersion
                };

                try
                {
                    DataContext.Employees.Remove(entity);
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
