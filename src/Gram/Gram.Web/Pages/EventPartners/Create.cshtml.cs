using Gram.Application.EventGuides.Queries;
using Gram.Application.EventPartners.Commands;
using Gram.Application.EventPartners.Models;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Gram.Web.Pages.EventPartners
{
    public class CreateModel : BasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public int EventId { get; set; }
        [BindProperty]
        public EventPartnerCreateModel Entity { get; set; }
        public SelectList ParetnersList { get; private set; }

        public async Task<IActionResult> OnGet()
        {
            ParetnersList = new SelectList(await Mediator.Send(new GetNewEventPartnersListQuery(EventId)), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ParetnersList = new SelectList(await Mediator.Send(new GetNewEventPartnersListQuery(EventId)), "Id", "Name");
                return Page();
            }

            await Mediator.Send(new CreateEventPartnerCommand(EventId, Entity));
            return RedirectToPage("/Events/Details", new { id = EventId });
        }
    }
}
