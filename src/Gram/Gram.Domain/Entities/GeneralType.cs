using Gram.Domain.Interfaces;
using System.Collections.Generic;

namespace Gram.Domain.Entities
{
    public sealed class GeneralType : IEntity
    {
        public GeneralType()
        {
            AttendanceStatuses = new HashSet<Attendance>();
            ChildTypes = new HashSet<GeneralType>();
            EventStatuses = new HashSet<Event>();
            PersonNationalities = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public bool IsListed { get; set; }
        public bool IsFixed { get; set; }
        public byte[] RowVersion { get; set; }

        public GeneralType Parent { get; set; }
        public ICollection<Attendance> AttendanceStatuses { get; }
        public ICollection<GeneralType> ChildTypes { get; }
        public ICollection<Event> EventStatuses { get; }
        public ICollection<Person> PersonNationalities { get; }
    }
}
