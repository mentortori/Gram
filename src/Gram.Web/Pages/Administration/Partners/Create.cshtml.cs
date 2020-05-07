using Gram.Application.Partners.Commands;
using Gram.Application.Partners.Models;
using Gram.Application.Partners.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Administration.Partners
{
    public class CreateModel : BasePageModel
    {
        [BindProperty]
        public PartnerCreateModel Entity { get; set; }
        public SelectList PeopleList { get; private set; }

        public CreateModel()
        {
            Entity = new PartnerCreateModel { IsActive = true };
        }

        public async Task<IActionResult> OnGet()
        {
            PeopleList = new SelectList(await Mediator.Send(new GetNewPartnersListQuery()), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PeopleList = new SelectList(await Mediator.Send(new GetNewPartnersListQuery()), "Id", "Name");
                return Page();
            }

            var id = await Mediator.Send(new CreatePartnerCommand(Entity, Mediator));
            return RedirectToPage("./Details", new { id = id });
        }
    }
}
