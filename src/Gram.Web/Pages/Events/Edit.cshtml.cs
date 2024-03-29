﻿using Gram.Application.Events.Commands;
using Gram.Application.Events.Models;
using Gram.Application.Events.Queries;
using Gram.Application.GeneralTypes.Queries;
using Gram.Domain.Enums;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Events
{
    public class EditModel : BasePageModel
    {
        [BindProperty]
        public EventEditModel Entity { get; set; }
        public SelectList StatusesList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetEventEditQuery(id.Value));
            StatusesList = new SelectList(await Mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.EventStatus)), "Id", "Title", Entity.EventStatusId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                StatusesList = new SelectList(await Mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.EventStatus)), "Id", "Title", Entity.EventStatusId);
                return Page();
            }

            await Mediator.Send(new UpdateEventCommand(Entity));
            return RedirectToPage("./Index");
        }
    }
}
