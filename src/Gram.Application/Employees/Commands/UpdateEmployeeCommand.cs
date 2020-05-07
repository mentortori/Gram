using Gram.Application.Abstraction;
using Gram.Application.Employees.Models;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest
    {
        private EmployeeEditModel Model { get; }

        public UpdateEmployeeCommand(EmployeeEditModel model)
        {
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<UpdateEmployeeCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                if ((await DataContext.Employees.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Model.Id, cancellationToken)) == null)
                    throw new EntityNotFoundException(nameof(Employee), request.Model.Id);

                var entity = new Employee
                {
                    Id = request.Model.Id,
                    RowVersion = request.Model.RowVersion,
                    PersonId = request.Model.PersonId,
                    DateOfEmployment = request.Model.DateOfEmployment,
                    EmploymentExpiryDate = request.Model.EmploymentExpiryDate
                };

                try
                {
                    DataContext.Employees.Attach(entity).State = EntityState.Modified;
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
