using Gram.Application.EventGuides.Commands;
using Gram.Application.EventGuides.Models;
using Gram.Application.Guides.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Gram.Web.Pages.EventGuides
{
    public class CreateModel : BasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public int EventId { get; set; }
        [BindProperty]
        public EventGuideCreateModel Entity { get; set; }
        public SelectList GuidesList { get; private set; }

        public async Task<IActionResult> OnGet()
        {
            GuidesList = new SelectList((await Mediator.Send(new GetNewEventGuidesListQuery(EventId))), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                GuidesList = new SelectList((await Mediator.Send(new GetNewEventGuidesListQuery(EventId))), "Id", "Name");
                return Page();
            }

            await Mediator.Send(new CreateEventGuideCommand(EventId, Entity));
            return RedirectToPage("/Events/Details", new { id = EventId });
        }
    }
}