using Gram.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Gram.Domain.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string UserId { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public DateTime? EmploymentExpiryDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Person Person { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
