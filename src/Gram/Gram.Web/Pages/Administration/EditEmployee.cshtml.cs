using Gram.Application.Employees.Commands;
using Gram.Application.Employees.Models;
using Gram.Application.Employees.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Administration
{
    public class EditEmployeeModel : BasePageModel
    {
        [BindProperty]
        public EmployeeEditModel Entity { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetEmployeeEditQuery(id.Value));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await Mediator.Send(new UpdateEmployeeCommand(Entity));
            return RedirectToPage("./Employees");
        }
    }
}
