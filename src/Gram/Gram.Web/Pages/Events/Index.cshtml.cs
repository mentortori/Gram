using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gram.Domain.Entities;
using Gram.Persistence;

namespace Gram.Web.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly Gram.Persistence.DataContext _context;

        public IndexModel(Gram.Persistence.DataContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }

        public async Task OnGetAsync()
        {
            Event = await _context.Events
                .Include(m => m.EventStatus).ToListAsync();
        }
    }
}
