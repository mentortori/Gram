using Gram.Application.EventPartners.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Shared.Components.EventPartners
{
    public class EventPartners : BaseComponentModel
    {
        public async Task<IViewComponentResult> InvokeAsync(int id) => View(await Mediator.Send(new GetEventPartnersQuery(id)));
    }
}
