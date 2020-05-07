using Gram.Application.Abstraction;
using Gram.Application.Employees.Models;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Employees.Queries
{
    public class GetEmployeeDeleteQuery : IRequest<EmployeeDeleteModel>
    {
        private int Id { get; }

        public GetEmployeeDeleteQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetEmployeeDeleteQuery, EmployeeDeleteModel>
        {
            private IEmployeeService EmployeeService { get; }

            public Handler(IDataContext dataContext, IEmployeeService employeeService) : base(dataContext)
            {
                EmployeeService = employeeService;
            }

            public async Task<EmployeeDeleteModel> Handle(GetEmployeeDeleteQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Employees
                    .Include(m => m.Person)
                        .ThenInclude(m => m.Nationality)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Employee), request.Id);

                return new EmployeeDeleteModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    IsDeletable = !EmployeeService.EmployeeHasUser(request.Id),
                    FirstName = entity.Person.FirstName,
                    LastName = entity.Person.LastName,
                    DateOfBirth = entity.Person.DateOfBirth,
                    Nationality = entity.Person.Nationality?.Title,
                    DateOfEmployment = entity.DateOfEmployment,
                    EmploymentExpiryDate = entity.EmploymentExpiryDate
                };
            }
        }
    }
}
