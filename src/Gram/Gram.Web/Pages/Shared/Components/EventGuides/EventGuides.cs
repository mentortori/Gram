using Gram.Application.EventGuides.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Shared.Components.EventGuides
{
    public class EventGuides : BaseComponentModel
    {
        public async Task<IViewComponentResult> InvokeAsync(int id) => View(await Mediator.Send(new GetEventGuidesQuery(id)));
    }
}
