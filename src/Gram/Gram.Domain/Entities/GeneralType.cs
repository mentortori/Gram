using Gram.Domain.Interfaces;
using System.Collections.Generic;

namespace Gram.Domain.Entities
{
    public sealed class GeneralType : IEntity
    {
        public GeneralType()
        {
            ChildTypes = new HashSet<GeneralType>();
            EventStatuses = new HashSet<Event>();
            PersonNationalities = new HashSet<Person>();
            ParticipationStatus = new HashSet<Participation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public bool IsListed { get; set; }
        public bool IsFixed { get; set; }
        public byte[] RowVersion { get; set; }

        public GeneralType Parent { get; set; }
        public ICollection<GeneralType> ChildTypes { get; }
        public ICollection<Event> EventStatuses { get; }
        public ICollection<Person> PersonNationalities { get; }
        public ICollection<Participation> ParticipationStatus { get; }
    }
}
