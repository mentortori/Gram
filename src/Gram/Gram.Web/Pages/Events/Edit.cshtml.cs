using Gram.Application.Events.Commands.UpdateEvent;
using Gram.Application.Events.Models;
using Gram.Application.Events.Queries;
using Gram.Application.GeneralTypes.Queries;
using Gram.Web.Pages.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static Gram.Domain.Enums.GeneralTypeEnum;

namespace Gram.Web.Pages.Events
{
    public class EditModel : BasePageModel
    {
        [BindProperty]
        public EventEditModel Entity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entity = await Mediator.Send(new GetEventEditQuery { Id = id.Value });

            if (Entity == null)
            {
                return NotFound();
            }

            ViewData["EventStatusId"] = new SelectList((await Mediator.Send(new GetDropDownListQuery((int)GeneralTypeParents.EventStatus))), "Id", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await Mediator.Send(new UpdateEventCommand { Id = id.Value, EventEditModel = Entity });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }

            return RedirectToPage("./Index");
        }
    }
}
