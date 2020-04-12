using Gram.Application.Abstraction;
using Gram.Application.Employees.Models;
using Gram.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.Employees.Queries
{
    public class GetEmployeesQuery : IRequest<List<EmployeeListViewModel>>
    {
        public class Handler : BaseHandler, IRequestHandler<GetEmployeesQuery, List<EmployeeListViewModel>>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<List<EmployeeListViewModel>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken) =>
                await DataContext.Employees
                    .Select(m => new EmployeeListViewModel
                    {
                        Id = m.Id,
                        FirstName = m.Person.FirstName,
                        LastName = m.Person.LastName,
                        DateOfBirth = m.Person.DateOfBirth,
                        DateOfEmployment = m.DateOfEmployment,
                        EmploymentExpiryDate = m.EmploymentExpiryDate
                    }).ToListAsync(cancellationToken);
        }
    }
}
