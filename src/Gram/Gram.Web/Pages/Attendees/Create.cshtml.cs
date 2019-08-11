using Gram.Application.Attendees.Models;
using Gram.Application.GeneralTypes.Queries;
using Gram.Application.People.Queries;
using Gram.Web.Pages.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using static Gram.Domain.Enums.GeneralTypeEnum;

namespace Gram.Web.Pages.Attendees
{
    public class CreateModel : BasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public int EventId { get; set; }

        [BindProperty]
        public AttendanceCreateModel Entity { get; set; }
        public SelectList CustomersList { get; private set; }
        public SelectList AttendanceStatusList { get; private set; }

        public async Task<IActionResult> OnGet()
        {
            CustomersList = new SelectList((await Mediator.Send(new GetPeopleDropDownListQuery(EventId))), "Id", "Name");
            AttendanceStatusList = new SelectList((await Mediator.Send(new GetDropDownListQuery((int)GeneralTypeParents.AttendanceStatus))), "Id", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CustomersList = new SelectList((await Mediator.Send(new GetPeopleDropDownListQuery(EventId))), "Id", "Name");
                AttendanceStatusList = new SelectList((await Mediator.Send(new GetDropDownListQuery((int)GeneralTypeParents.AttendanceStatus))), "Id", "Title");
                return Page();
            }

            //await Mediator.Send(new CreatePersonCommand(Entity));
            return RedirectToPage("../Events/Details", new { id = EventId });
        }
    }
}