using Gram.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Gram.Domain.Entities
{
    public sealed class Event : IEntity
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

        public GeneralType EventStatus { get; set; }
        public ICollection<Participation> EventParticipations { get; }
    }
}
