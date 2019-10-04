using Gram.Application.EventPartners.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Components.EventPartners
{
    public class EventPartnersViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public EventPartnersViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View(await _mediator.Send(new GetEventPartnersQuery(id)));
        }
    }
}
