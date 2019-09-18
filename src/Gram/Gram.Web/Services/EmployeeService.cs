using Gram.Application.Interfaces;
using Gram.Web.Areas.Identity;
using System.Linq;

namespace Gram.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IdentityContext IdentityContext { get; private set; }

        public EmployeeService(IdentityContext identityContext)
        {
            IdentityContext = identityContext;
        }

        public bool EmployeeHasUser(int id)
        {
            return IdentityContext.Users.Any(m => m.EmployeeId == id);
        }
    }
}
