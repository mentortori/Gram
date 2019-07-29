using Gram.Application.Events.Models;
using Gram.Application.Events.Queries;
using Gram.Web.Pages.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Events
{
    public class IndexModel : BasePageModel
    {
        public IList<EventsListViewModel> Event { get;set; }

        public async Task OnGetAsync()
        {
            Event = await Mediator.Send(new GetAllEventsQuery());
        }
    }
}
