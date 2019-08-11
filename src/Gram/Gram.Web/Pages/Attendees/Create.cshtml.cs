using System;
using Gram.Application.Attendees.Models;
using Gram.Application.GeneralTypes.Queries;
using Gram.Application.People.Queries;
using Gram.Web.Pages.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Gram.Application.Attendees.Commands;
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

        public CreateModel()
        {
            Entity = new AttendanceCreateModel { StatusDate = DateTime.Now };
        }

        public async Task<IActionResult> OnGet()
        {
            CustomersList = new SelectList((await Mediator.Send(new GetNewAttendeesListQuery(EventId))), "Id", "Name");
            AttendanceStatusList = new SelectList((await Mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.AttendanceStatus))), "Id", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CustomersList = new SelectList((await Mediator.Send(new GetNewAttendeesListQuery(EventId))), "Id", "Name");
                AttendanceStatusList = new SelectList((await Mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.AttendanceStatus))), "Id", "Title");
                return Page();
            }

            await Mediator.Send(new CreateAttendanceCommand(EventId, Entity));
            return RedirectToPage("../Events/Details", new { id = EventId });
        }
    }
}