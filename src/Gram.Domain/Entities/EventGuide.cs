using Gram.Domain.Interfaces;

namespace Gram.Domain.Entities
{
    public sealed class EventGuide : IEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int GuideId { get; set; }
        public byte[] RowVersion { get; set; }

        public Event Event { get; set; }
        public Guide Guide { get; set; }
    }
}
