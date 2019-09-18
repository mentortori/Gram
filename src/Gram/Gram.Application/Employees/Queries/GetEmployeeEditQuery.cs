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
    public class GetEmployeeEditQuery : IRequest<EmployeeEditModel>
    {
        private int Id { get; }

        public GetEmployeeEditQuery(int id)
        {
            Id = id;
        }

        public class Handler : BaseHandler, IRequestHandler<GetEmployeeEditQuery, EmployeeEditModel>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<EmployeeEditModel> Handle(GetEmployeeEditQuery request, CancellationToken cancellationToken)
            {
                var entity = await DataContext.Employees
                    .Include(m => m.Person)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new EntityNotFoundException(nameof(Employee), request.Id);

                return new EmployeeEditModel
                {
                    Id = request.Id,
                    RowVersion = entity.RowVersion,
                    PersonId = entity.PersonId,
                    Name = entity.Person.FirstName + " " + entity.Person.LastName,
                    DateOfEmployment = entity.EmploymentExpiryDate,
                    EmploymentExpiryDate = entity.EmploymentExpiryDate
                };
            }
        }
    }
}
