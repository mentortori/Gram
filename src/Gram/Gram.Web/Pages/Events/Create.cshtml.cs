using Gram.Application.Events.Commands.CreateEvent;
using Gram.Application.GeneralTypes.Queries;
using Gram.Web.Pages.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Events
{
    public class CreateModel : BasePageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["EventStatusId"] = new SelectList((await Mediator.Send(new GetAllEventStatusesQuery())), "Id", "Title");
            return Page();
        }

        [BindProperty]
        public CreateEventCommand Command { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await Mediator.Send(Command);
            return RedirectToPage("./Index");
        }
    }
}