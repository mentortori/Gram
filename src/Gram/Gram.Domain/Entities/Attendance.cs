using Gram.Domain.Interfaces;
using System;

namespace Gram.Domain.Entities
{
    public sealed class Attendance : IEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int PersonId { get; set; }
        public int StatusId { get; set; }
        public DateTime StatusDate { get; set; }
        public string Remarks { get; set; }
        public byte[] RowVersion { get; set; }

        public Event Event { get; set; }
        public Person Person { get; set; }
        public GeneralType Status { get; set; }
    }
}
