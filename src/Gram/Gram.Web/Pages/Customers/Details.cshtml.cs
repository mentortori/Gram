using Gram.Application.People.Models;
using Gram.Application.People.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Customers
{
    public class DetailsModel : BasePageModel
    {
        public PersonDetailModel Entity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetPersonDetailQuery(id.Value));
            return Page();
        }
    }
}
