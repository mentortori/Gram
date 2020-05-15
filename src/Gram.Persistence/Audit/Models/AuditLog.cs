using System;
using System.Collections.Generic;

namespace Gram.Persistence.Audit.Models
{
    public class AuditLog
    {
        public AuditLog()
        {
            AuditDetails = new HashSet<AuditDetail>();
        }

        public int Id { get; set; }
        public string EntityState { get; set; }
        public string Entity { get; set; }
        public int EntityId { get; set; }
        public string RowModifyUser { get; set; }
        public DateTime RowModifyDate { get; set; }

        public ICollection<AuditDetail> AuditDetails { get; set; }
    }
}
