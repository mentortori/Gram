using Gram.Application.Partners.Models;
using Gram.Application.Partners.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Administration.Partners
{
    [Authorize(Roles = "Administrator, Employee")]
    public class IndexModel : BasePageModel
    {
        public IList<PartnerListViewModel> Entity { get; set; }

        public async Task OnGet()
        {
            Entity = await Mediator.Send(new GetPartnersQuery());
        }
    }
}
