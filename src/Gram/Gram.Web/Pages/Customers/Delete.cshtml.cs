using Gram.Application.People.Commands;
using Gram.Application.People.Models;
using Gram.Application.People.Queries;
using Gram.Web.Pages.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Customers
{
    public class DeleteModel : BasePageModel
    {
        [BindProperty]
        public PersonDetailModel Entity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetPersonDetailQuery { Id = id.Value });
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                await Mediator.Send(new DeletePersonCommand { Id = id.Value, RowVersion = Entity.RowVersion });
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
    }
}
