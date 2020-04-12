using Gram.Application.Attendees.Commands;
using Gram.Application.Attendees.Models;
using Gram.Application.Attendees.Queries;
using Gram.Application.GeneralTypes.Queries;
using Gram.Domain.Enums;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Attendees
{
    public class EditModel : BasePageModel
    {
        [BindProperty]
        public UpdateDto Entity { get; set; }
        public SelectList AttendanceStatusList { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetAttendanceEditQuery(id.Value));
            AttendanceStatusList = new SelectList((await Mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.AttendanceStatus))), "Id", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                AttendanceStatusList = new SelectList((await Mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.AttendanceStatus))), "Id", "Title");
                return Page();
            }

            await Mediator.Send(new UpdateAttendanceCommand(Entity));
            return RedirectToPage("/Events/Details", new { id = Entity.EventId });
        }
    }
}
