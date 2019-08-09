using Gram.Application.Abstraction;
using Gram.Application.Interfaces;
using Gram.Application.People.Models;
using Gram.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gram.Application.People.Commands
{
    public class CreatePersonCommand : IRequest
    {
        public PersonCreateModel Model { get; set; }

        public class Handler : BaseHandler, IRequestHandler<CreatePersonCommand, Unit>
        {
            public Handler(IDataContext dataContext) : base(dataContext)
            {
            }

            public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
            {
                var entity = new Person
                {
                    FirstName = request.Model.FirstName,
                    LastName = request.Model.LastName,
                    DateOfBirth = request.Model.DateOfBirth,
                    NationalityId = request.Model.NationalityId
                };

                await DataContext.People.AddAsync(entity, cancellationToken);
                await DataContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
