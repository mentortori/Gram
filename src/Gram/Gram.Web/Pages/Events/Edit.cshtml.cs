using Gram.Application.Events.Commands;
using Gram.Application.Events.Models;
using Gram.Application.Events.Queries;
using Gram.Application.GeneralTypes.Queries;
using Gram.Web.Pages.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using static Gram.Domain.Enums.GeneralTypeEnum;

namespace Gram.Web.Pages.Events
{
    public class EditModel : BasePageModel
    {
        [BindProperty]
        public EventEditModel Entity { get; set; }
        public SelectList StatusesList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetEventEditQuery { Id = id.Value });
            StatusesList = new SelectList((await Mediator.Send(new GetDropDownListQuery((int)GeneralTypeParents.EventStatus))), "Id", "Title", Entity.EventStatusId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                StatusesList = new SelectList((await Mediator.Send(new GetDropDownListQuery((int)GeneralTypeParents.EventStatus))), "Id", "Title", Entity.EventStatusId);
                return Page();
            }

            await Mediator.Send(new UpdateEventCommand { Id = id.Value, Model = Entity });
            return RedirectToPage("./Index");
        }
    }
}
