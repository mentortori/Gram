using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gram.Domain.Entities;
using Gram.Persistence;

namespace Gram.Web.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly Gram.Persistence.DataContext _context;

        public IndexModel(Gram.Persistence.DataContext context)
        {
            _context = context;
        }

        public IList<Person> Person { get;set; }

        public async Task OnGetAsync()
        {
            Person = await _context.People
                .Include(p => p.Nationality).ToListAsync();
        }
    }
}
