using Gram.Application.Events.Commands.CreateEvent;
using Gram.Application.Events.Models;
using Gram.Application.GeneralTypes.Queries;
using Gram.Web.Pages.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using static Gram.Domain.Enums.GeneralTypeEnum;

namespace Gram.Web.Pages.Events
{
    public class CreateModel : BasePageModel
    {
        public SelectList StatusesList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            StatusesList = new SelectList((await Mediator.Send(new GetDropDownListQuery((int)GeneralTypeParents.EventStatus))), "Id", "Title");
            return Page();
        }

        [BindProperty]
        public EventCreateModel Entity { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await Mediator.Send(new CreateEventCommand { EventCreateModel = Entity });
            return RedirectToPage("./Index");
        }
    }
}
