using System.Threading.Tasks;
using Gram.Application.EventPartners.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gram.Web.Pages.Shared.Components.EventPartners
{
    public class EventPartners : ViewComponent
    {
        private readonly IMediator _mediator;

        public EventPartners(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View(await _mediator.Send(new GetEventPartnersQuery(id)));
        }
    }
}
