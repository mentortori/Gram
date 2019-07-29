using System.Collections.Generic;
using Gram.Application.Events.Models;
using MediatR;

namespace Gram.Application.Events.Queries
{
    public class GetAllEventsQuery : IRequest<List<EventsListViewModel>>
    {
    }
}
