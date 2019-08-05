using Gram.Application.Events.Commands.DeleteEvent;
using Gram.Application.Events.Models;
using Gram.Application.Events.Queries;
using Gram.Web.Pages.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Events
{
    public class DeleteModel : BasePageModel
    {
        [BindProperty]
        public EventDetailModel Entity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetEventDetailQuery { Id = id.Value });
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                await Mediator.Send(new DeleteEventCommand { Id = id.Value, RowVersion = Entity.RowVersion });
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
    }
}
