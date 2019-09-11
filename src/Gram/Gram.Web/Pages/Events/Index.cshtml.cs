using Gram.Application.Events.Models;
using Gram.Application.Events.Queries;
using Gram.Web.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Events
{
    public class IndexModel : BasePageModel
    {
        public IList<EventsListViewModel> Entity { get;set; }

        public async Task OnGetAsync()
        {
            Entity = await Mediator.Send(new GetAllEventsQuery());
        }
    }
}
