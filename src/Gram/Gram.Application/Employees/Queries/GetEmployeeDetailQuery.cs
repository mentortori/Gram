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
    public class GetEmployeeDetailQuery : IRequest<EmployeeDetailModel>
    {
        private int Id { get; }

        public GetEmployeeDetailQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetEmployeeDetailQuery, EmployeeDetailModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<EmployeeDetailModel> Handle(GetEmployeeDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Employees
                    .Include(m => m.Person)
                        .ThenInclude(m => m.Nationality)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Employee), request.Id);

                return new EmployeeDetailModel
                {
                    Id = request.Id,
                    Name = entity.Person.FirstName + " " + entity.Person.LastName,
                    DateOfBirth = entity.Person.DateOfBirth,
                    Nationality = entity.Person.Nationality?.Title,
                    DateOfEmployment = entity.DateOfEmployment,
                    EmploymentExpiryDate = entity.EmploymentExpiryDate
                };
            }
        }
    }
}
