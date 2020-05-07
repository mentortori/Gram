using Gram.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Gram.Domain.Entities
{
    public sealed class Person : IEntity
    {
        public Person()
        {
            Attendees = new HashSet<Attendance>();
            Employees = new HashSet<Employee>();
            Guides = new HashSet<Guide>();
            PersonContactInfos = new HashSet<PersonContactInfo>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? NationalityId { get; set; }
        public byte[] RowVersion { get; set; }

        public GeneralType Nationality { get; set; }
        public ICollection<Attendance> Attendees { get; }
        public ICollection<Employee> Employees { get; }
        public ICollection<Guide> Guides { get; }
        public ICollection<PersonContactInfo> PersonContactInfos { get; }
    }
}
