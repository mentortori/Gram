using Gram.Application.Abstraction;
using Gram.Application.Employees.Models;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Employees.Commands
{
    public class CreateEmployeeCommand : IRequest<int>
    {
        private EmployeeCreateModel Model { get; }

        public CreateEmployeeCommand(EmployeeCreateModel model)
        {
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<CreateEmployeeCommand, int>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var entity = new Employee
                {
                    PersonId = request.Model.PersonId,
                    DateOfEmployment = request.Model.DateOfEmployment,
                    EmploymentExpiryDate = request.Model.EmploymentExpiryDate
                };

                await DataContext.Employees.AddAsync(entity, cancellationToken);
                await DataContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}
