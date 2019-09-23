using Gram.Application.Partners.Commands;
using Gram.Application.Partners.Models;
using Gram.Application.Partners.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Administration.Partners
{
    public class EditModel : BasePageModel
    {
        [BindProperty]
        public PartnerEditModel Entity { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetPartnerEditQuery(id.Value));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await Mediator.Send(new UpdatePartnerCommand(Entity));
            return RedirectToPage("./Details", new { id = id });
        }
    }
}
