using Gram.Application.Employees.Commands;
using Gram.Application.Employees.Models;
using Gram.Application.Employees.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Administration.Employees
{
    public class CreateModel : BasePageModel
    {
        [BindProperty]
        public EmployeeCreateModel Entity { get; set; }
        public SelectList PeopleList { get; private set; }

        public async Task<IActionResult> OnGet()
        {
            PeopleList = new SelectList(await Mediator.Send(new GetNewEmployeesListQuery()), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PeopleList = new SelectList(await Mediator.Send(new GetNewEmployeesListQuery()), "Id", "Name");
                return Page();
            }

            var id = await Mediator.Send(new CreateEmployeeCommand(Entity));
            return RedirectToPage("./Details", new { id = id });
        }
    }
}
