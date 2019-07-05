﻿using Gram.Core.Interfaces;
using System.Collections.Generic;

namespace Gram.Core.Entities
{
    public class GeneralType : IEntity
    {
        public GeneralType()
        {
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

        public virtual GeneralType Parent { get; set; }
        public virtual ICollection<GeneralType> ChildTypes { get; set; }
        public virtual ICollection<Event> EventStatuses { get; set; }
        public virtual ICollection<Person> PersonNationalities { get; set; }
    }
}
