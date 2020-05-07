using Gram.Domain.Interfaces;

namespace Gram.Domain.Entities
{
    public sealed class EventPartner : IEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int PartnerId { get; set; }
        public byte[] RowVersion { get; set; }

        public Event Event { get; set; }
        public Partner Partner { get; set; }
    }
}
