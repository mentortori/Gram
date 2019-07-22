using Gram.Core.Interfaces;
using System;

namespace Gram.Core.Entities
{
    public class Participation : IEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int PersonId { get; set; }
        public int StatusId { get; set; }
        public DateTime StatusDate { get; set; }
        public string Remarks { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Event Event { get; set; }
        public virtual Person Person { get; set; }
        public virtual GeneralType Status { get; set; }
    }
}
