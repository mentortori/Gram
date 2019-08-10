using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gram.Domain.Entities;
using Gram.Persistence;

namespace Gram.Web.Pages.Attendees
{
    public class DeleteModel : PageModel
    {
        private readonly Gram.Persistence.DataContext _context;

        public DeleteModel(Gram.Persistence.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Attendance Attendance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Attendance = await _context.Attendees
                .Include(a => a.Event)
                .Include(a => a.Person)
                .Include(a => a.Status).FirstOrDefaultAsync(m => m.Id == id);

            if (Attendance == null)
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

            Attendance = await _context.Attendees.FindAsync(id);

            if (Attendance != null)
            {
                _context.Attendees.Remove(Attendance);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
