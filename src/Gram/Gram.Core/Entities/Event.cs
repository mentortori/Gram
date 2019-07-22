using Gram.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Gram.Core.Entities
{
    public class Event : IEntity
    {
        public Event()
        {
            EventParticipations = new HashSet<Participation>();
        }

        public int Id { get; set; }
        public string EventName { get; set; }
        public int EventStatusId { get; set; }
        public string EventDescription { get; set; }
        public DateTime? EventDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual GeneralType EventStatus { get; set; }
        public virtual ICollection<Participation> EventParticipations { get; set; }
    }
}
