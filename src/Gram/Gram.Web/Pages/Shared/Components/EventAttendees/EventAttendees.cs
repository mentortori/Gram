using Gram.Application.Attendees.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Shared.Components.EventAttendees
{
    public class EventAttendees : BaseComponentModel
    {
        public async Task<IViewComponentResult> InvokeAsync(int id) => View(await Mediator.Send(new GetEventAttendeesQuery(id)));
    }
}
