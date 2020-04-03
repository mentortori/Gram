using System.Threading.Tasks;
using Gram.Application.EventGuides.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gram.Web.Pages.Shared.Components.EventGuides
{
    public class EventGuides : ViewComponent
    {
        private readonly IMediator _mediator;

        public EventGuides(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View(await _mediator.Send(new GetEventGuidesQuery(id)));
        }
    }
}
