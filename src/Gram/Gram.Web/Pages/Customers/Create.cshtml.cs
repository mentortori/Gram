using Gram.Application.GeneralTypes.Queries;
using Gram.Application.People.Commands;
using Gram.Application.People.Models;
using Gram.Web.Pages.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using static Gram.Domain.Enums.GeneralTypeEnum;

namespace Gram.Web.Pages.Customers
{
    public class CreateModel : BasePageModel
    {
        [BindProperty]
        public PersonCreateModel Entity { get; set; }
        public SelectList NationalitiesList { get; set; }

        public async Task<IActionResult> OnGet()
        {
            NationalitiesList = new SelectList((await Mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.Nationality))), "Id", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                NationalitiesList = new SelectList((await Mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.Nationality))), "Id", "Title");
                return Page();
            }

            await Mediator.Send(new CreatePersonCommand(Entity));
            return RedirectToPage("./Index");
        }
    }
}
