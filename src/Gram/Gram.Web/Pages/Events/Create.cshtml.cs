using Gram.Application.Events.Commands.CreateEvent;
using Gram.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Events
{
    public class CreateModel : BasePageModel
    {
        private readonly IDataContext _context;

        public CreateModel(IDataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EventStatusId"] = new SelectList(_context.GeneralTypes, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public CreateEventCommand Event { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await Mediator.Send(Event);

            return RedirectToPage("./Index");
        }
    }

    public abstract class BasePageModel : PageModel
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}