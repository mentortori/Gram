using Gram.Application.Attendees.Commands;
using Gram.Application.Attendees.Models;
using Gram.Application.Attendees.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Attendees
{
    public class DeleteModel : BasePageModel
    {
        [BindProperty]
        public AttendanceDeleteModel Entity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetAttendanceDeleteQuery(id.Value));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await Mediator.Send(new DeleteAttendanceCommand(id.Value, Entity.RowVersion));
            return RedirectToPage("/Events/Details", new { id = Entity.EventId });
        }
    }
}
