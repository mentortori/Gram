using Gram.Application.Guides.Commands;
using Gram.Application.Guides.Models;
using Gram.Application.Guides.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Administration
{
    public class DeleteGuideModel : BasePageModel
    {
        [BindProperty]
        public GuideDeleteModel Entity { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetGuideDeleteQuery(id.Value));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await Mediator.Send(new DeleteGuideCommand(id.Value, Entity.RowVersion));
            return RedirectToPage("./Guides");
        }
    }
}
