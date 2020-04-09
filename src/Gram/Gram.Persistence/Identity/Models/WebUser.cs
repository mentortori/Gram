using Gram.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gram.Persistence.Identity.Models
{
    public class WebUser : IdentityUser
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
