using Gram.Application.Interfaces;
using Gram.Persistence.Identity;
using System.Linq;

namespace Gram.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IdentityContext _identityContext;

        public EmployeeService(IdentityContext identityContext) => _identityContext = identityContext;

        public bool EmployeeHasUser(int id) => _identityContext.Users.Any(m => m.EmployeeId == id);
    }
}
