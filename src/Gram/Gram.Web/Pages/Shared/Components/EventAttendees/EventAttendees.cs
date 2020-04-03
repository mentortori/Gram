using System.Threading.Tasks;
using Gram.Application.Attendees.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gram.Web.Pages.Shared.Components.EventAttendees
{
    public class EventAttendees : ViewComponent
    {
        private readonly IMediator _mediator;

        public EventAttendees(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View(await _mediator.Send(new GetEventAttendeesQuery(id)));
        }
    }
}
