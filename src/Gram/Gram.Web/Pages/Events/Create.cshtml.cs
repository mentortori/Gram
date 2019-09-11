using Gram.Application.Events.Commands;
using Gram.Application.Events.Models;
using Gram.Application.GeneralTypes.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using static Gram.Domain.Enums.GeneralTypeEnum;

namespace Gram.Web.Pages.Events
{
    public class CreateModel : BasePageModel
    {
        [BindProperty]
        public EventCreateModel Entity { get; set; }
        public SelectList StatusesList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            StatusesList = new SelectList((await Mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.EventStatus))), "Id", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                StatusesList = new SelectList((await Mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.EventStatus))), "Id", "Title");
                return Page();
            }

            var id = await Mediator.Send(new CreateEventCommand(Entity));
            return RedirectToPage("./Details", new { id = id });
        }
    }
}
