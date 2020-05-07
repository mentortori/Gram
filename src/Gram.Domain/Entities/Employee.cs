using Gram.Domain.Interfaces;
using System;

namespace Gram.Domain.Entities
{
    public sealed class Employee : IEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public DateTime? EmploymentExpiryDate { get; set; }
        public byte[] RowVersion { get; set; }

        public Person Person { get; set; }
    }
}
