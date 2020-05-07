using Gram.Application.Guides.Models;
using Gram.Application.Guides.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Administration.Guides
{
    public class Details: BasePageModel
    {
        public GuideDetailModel Entity { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetGuideDetailQuery(id.Value));
            return Page();
        }
    }
}
