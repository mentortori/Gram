using Gram.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Gram.Core.Entities
{
    public class Person : IEntity
    {
        public Person()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? NationalityId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual GeneralType Nationality { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
