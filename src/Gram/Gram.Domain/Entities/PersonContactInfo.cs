using Gram.Domain.Interfaces;

namespace Gram.Domain.Entities
{
    public sealed class PersonContactInfo : IEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ContactTypeId { get; set; }
        public string Content { get; set; }
        public byte[] RowVersion { get; set; }

        public Person Person { get; set; }
        public GeneralType ContactType { get; set; }
    }
}
