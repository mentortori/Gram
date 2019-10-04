using Gram.Application.EventGuides.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Components.EventGuides
{
    public class EventGuidesViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public EventGuidesViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View(await _mediator.Send(new GetEventGuidesQuery(id)));
        }
    }
}
