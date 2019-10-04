using Gram.Application.EventPartners.Commands;
using Gram.Application.SharedModels;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.EventPartners
{
    [ValidateAntiForgeryToken]
    public class DeleteModel : BasePageModel
    {
        [BindProperty]
        public ListItemWithRowVersionModel Entity { get; set; }

        public async Task<IActionResult> OnPostAsync(int id, byte[] rowVersion, int eventId)
        {
            if (id == 0)
                return NotFound();

            await Mediator.Send(new DeleteEventPartnerCommand(id, rowVersion));
            return RedirectToPage("../Events/Details/", new { id = eventId });
        }
    }
}
