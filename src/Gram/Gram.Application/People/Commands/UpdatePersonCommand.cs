using Gram.Application.Abstraction;
using Gram.Application.Exceptions;
using Gram.Application.Interfaces;
using Gram.Application.People.Models;
using Gram.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.People.Commands
{
    public class UpdatePersonCommand : IRequest
    {
        private PersonEditModel Model { get; }

        public UpdatePersonCommand(PersonEditModel model)
        {
            Model = model;
        }

        public class Handler : BaseHandler, IRequestHandler<UpdatePersonCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
            {
                if ((await DataContext.People.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Model.Id, cancellationToken)) == null)
                    throw new EntityNotFoundException(nameof(Person), request.Model.Id);

                var entity = new Person
                {
                    Id = request.Model.Id,
                    RowVersion = request.Model.RowVersion,
                    FirstName = request.Model.FirstName,
                    LastName = request.Model.LastName,
                    DateOfBirth = request.Model.DateOfBirth,
                    NationalityId = request.Model.NationalityId
                };

                try
                {
                    DataContext.People.Attach(entity).State = EntityState.Modified;
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
