using Gram.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Gram.Domain.Entities
{
    public sealed class Guide : IEntity
    {
        public Guide()
        {
            EventGuides = new HashSet<EventGuide>();
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public bool IsActive { get; set; }
        public byte[] RowVersion { get; set; }

        public Person Person { get; set; }
        public ICollection<EventGuide> EventGuides { get; }
    }
}
