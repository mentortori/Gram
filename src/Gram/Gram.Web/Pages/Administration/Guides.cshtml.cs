using Gram.Application.Guides.Models;
using Gram.Application.Guides.Queries;
using Gram.Web.Abstraction;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gram.Web.Pages.Administration
{
    [Authorize(Roles = "Administrator, Employee")]
    public class GuidesModel : BasePageModel
    {
        public IList<GuideListViewModel> Entity { get; set; }

        public async Task OnGet()
        {
            Entity = await Mediator.Send(new GetGuidesQuery());
        }
    }
}
