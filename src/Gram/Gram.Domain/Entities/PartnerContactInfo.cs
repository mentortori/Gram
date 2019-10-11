using System.Collections.Generic;
using Gram.Domain.Interfaces;

namespace Gram.Domain.Entities
{
    public class PartnerContactInfo : IEntity
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public int ContactTypeId { get; set; }
        public string Content { get; set; }
        public byte[] RowVersion { get; set; }

        public Partner Partner { get; set; }
        public GeneralType ContactType { get; set; }
    }
}
