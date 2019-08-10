using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gram.Domain.Entities;
using Gram.Persistence;

namespace Gram.Web.Pages.Attendees
{
    public class EditModel : PageModel
    {
        private readonly Gram.Persistence.DataContext _context;

        public EditModel(Gram.Persistence.DataContext context)
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
           ViewData["EventId"] = new SelectList(_context.Events, "Id", "EventDescription");
           ViewData["PersonId"] = new SelectList(_context.People, "Id", "FirstName");
           ViewData["StatusId"] = new SelectList(_context.GeneralTypes, "Id", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(Attendance.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendees.Any(e => e.Id == id);
        }
    }
}
