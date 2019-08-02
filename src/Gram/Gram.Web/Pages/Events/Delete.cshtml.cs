using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gram.Domain.Entities;
using Gram.Persistence;
using Gram.Application.Events.Models;
using Gram.Web.Pages.Abstraction;
using Gram.Application.Events.Queries;
using Gram.Application.Events.Commands.DeleteEvent;

namespace Gram.Web.Pages.Events
{
    public class DeleteModel : BasePageModel
    {
        [BindProperty]
        public EventDetailModel Entity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entity = await Mediator.Send(new GetEventDetailQuery { Id = id.Value });

            if (Entity == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entity = await Mediator.Send(new GetEventDetailQuery { Id = id.Value });

            if (Entity != null)
            {
                await Mediator.Send(new DeleteEventCommand { Id = id.Value });
            }

            return RedirectToPage("./Index");
        }
    }
}
