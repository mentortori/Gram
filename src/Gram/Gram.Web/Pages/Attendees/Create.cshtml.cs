using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gram.Domain.Entities;
using Gram.Persistence;

namespace Gram.Web.Pages.Attendees
{
    public class CreateModel : PageModel
    {
        private readonly Gram.Persistence.DataContext _context;

        public CreateModel(Gram.Persistence.DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EventId"] = new SelectList(_context.Events, "Id", "EventDescription");
        ViewData["PersonId"] = new SelectList(_context.People, "Id", "FirstName");
        ViewData["StatusId"] = new SelectList(_context.GeneralTypes, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Attendance Attendance { get; set; }

        [BindProperty(SupportsGet = true)]
        public int EventId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attendees.Add(Attendance);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}