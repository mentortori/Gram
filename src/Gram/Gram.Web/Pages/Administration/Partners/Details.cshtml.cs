using Gram.Application.Partners.Models;
using Gram.Application.Partners.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Administration.Partners
{
    public class Details: BasePageModel
    {
        public PartnerDetailModel Entity { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Entity = await Mediator.Send(new GetPartnerDetailQuery(id.Value));
            return Page();
        }
    }
}
