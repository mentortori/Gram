using Gram.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Gram.Web.Services
{
    public class UserService : IUserService
    {
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            UserName = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Unauthenticated";
        }

        public string UserName { get; }
    }
}
