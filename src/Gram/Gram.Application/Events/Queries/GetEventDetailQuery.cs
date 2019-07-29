using Gram.Application.Events.Models;
using MediatR;

namespace Gram.Application.Events.Queries
{
    public class GetEventDetailQuery : IRequest<EventDetailModel>
    {
        public int Id { get; set; }
    }
}
