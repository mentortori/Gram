using Gram.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Gram.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUser() => _httpContextAccessor.HttpContext.User?.Identity?.Name ?? "Unauthenticated";
    }
}
