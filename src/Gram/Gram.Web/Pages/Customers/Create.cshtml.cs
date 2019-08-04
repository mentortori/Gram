using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gram.Domain.Entities;
using Gram.Persistence;

namespace Gram.Web.Pages.Customers
{
    public class CreateModel : PageModel
    {
        public SelectList NationalitiesList { get; set; }
        private readonly Gram.Persistence.DataContext _context;

        public CreateModel(Gram.Persistence.DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            NationalitiesList = new SelectList(_context.GeneralTypes.Where(p => p.Parent.Title == "Nationality"), "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Person Entity { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.People.Add(Entity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}