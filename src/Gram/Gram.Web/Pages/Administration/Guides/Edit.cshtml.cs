using Gram.Application.Guides.Commands;
using Gram.Application.Guides.Models;
using Gram.Application.Guides.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Administration.Guides
{
    public class EditModel : BasePageModel
    {
        [BindProperty]
        public GuideEditModel Entity { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetGuideEditQuery(id.Value));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await Mediator.Send(new UpdateGuideCommand(Entity));
            return RedirectToPage("./Details", new { id = id });
        }
    }
}
