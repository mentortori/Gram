using Gram.Application.People.Models;
using Gram.Application.People.Queries;
using Gram.Web.Pages.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Customers
{
    public class IndexModel : BasePageModel
    {
        public IList<PersonListViewModel> Entity { get; set; }

        public async Task OnGetAsync()
        {
            Entity = await Mediator.Send(new GetAllPeopleQuery());
        }
    }
}
