using Gram.Application.Events.Commands;
using Gram.Application.Events.Models;
using Gram.Application.Events.Queries;
using Gram.Web.Pages.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Events
{
    public class DeleteModel : BasePageModel
    {
        [BindProperty]
        public EventDeleteModel Entity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetEventDeleteQuery(id.Value));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await Mediator.Send(new DeleteEventCommand(id.Value, Entity.RowVersion));
            return RedirectToPage("./Index");
        }
    }
}
