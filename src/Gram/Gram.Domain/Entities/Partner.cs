using Gram.Domain.Interfaces;
using System.Collections.Generic;

namespace Gram.Domain.Entities
{
    public sealed class Partner : IEntity
    {
        public Partner()
        {
            EventPartners = new HashSet<EventPartner>();
            PartnerContactInfos = new HashSet<PartnerContactInfo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public byte[] RowVersion { get; set; }

        public ICollection<EventPartner> EventPartners { get; }
        public ICollection<PartnerContactInfo> PartnerContactInfos { get; }
    }
}
