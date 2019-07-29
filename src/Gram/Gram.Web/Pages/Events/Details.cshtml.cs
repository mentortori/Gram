using Gram.Application.Events.Models;
using Gram.Application.Events.Queries;
using Gram.Web.Pages.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Events
{
    public class DetailsModel : BasePageModel
    {
        public EventDetailModel Entity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entity = await Mediator.Send(new GetEventDetailQuery { Id = id.Value });

            if (Entity == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
