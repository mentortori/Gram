﻿using Gram.Application.GeneralTypes.Queries;
using Gram.Application.People.Commands;
using Gram.Application.People.Models;
using Gram.Application.People.Queries;
using Gram.Domain.Enums;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Customers
{
    public class EditModel : BasePageModel
    {
        [BindProperty]
        public PersonEditModel Entity { get; set; }
        public SelectList NationalitiesList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetPersonEditQuery(id.Value, Mediator));
            NationalitiesList = new SelectList(await Mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.Nationality)), "Id", "Title", Entity.NationalityId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                NationalitiesList = new SelectList(await Mediator.Send(new GetGeneralTypesListQuery((int)GeneralTypeParents.Nationality)), "Id", "Title", Entity.NationalityId);
                return Page();
            }

            await Mediator.Send(new UpdatePersonCommand(Entity, Mediator));
            return RedirectToPage("./Index");
        }
    }
}
