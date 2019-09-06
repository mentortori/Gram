using Gram.Application.EventGuides.Commands;
using Gram.Application.EventGuides.Models;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.EventGuides
{
    [ValidateAntiForgeryToken]
    public class DeleteModel : BasePageModel
    {
        [BindProperty]
        public EventGuideModel Entity { get; set; }

        public async Task<IActionResult> OnPostAsync(int id, byte[] rowVersion, int eventId)
        {
            if (id == 0)
                return NotFound();

            await Mediator.Send(new DeleteEventGuideCommand(id, rowVersion));
            return RedirectToPage("../Events/Details/", new { id = eventId });
        }
    }
}
