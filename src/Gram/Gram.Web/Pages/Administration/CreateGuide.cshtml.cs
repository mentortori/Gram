using Gram.Application.Guides.Commands;
using Gram.Application.Guides.Models;
using Gram.Application.Guides.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Administration
{
    public class CreateGuideModel : BasePageModel
    {
        [BindProperty]
        public GuideCreateModel Entity { get; set; }
        public SelectList PeopleList { get; private set; }

        public CreateGuideModel()
        {
            Entity = new GuideCreateModel { IsActive = true };
        }

        public async Task<IActionResult> OnGet()
        {
            PeopleList = new SelectList((await Mediator.Send(new GetNewGuidesListQuery())), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PeopleList = new SelectList((await Mediator.Send(new GetNewGuidesListQuery())), "Id", "Name");
                return Page();
            }

            var id = await Mediator.Send(new CreateGuideCommand(Entity));
            return RedirectToPage("./DetailsGuide", new { id = id });
        }
    }
}
