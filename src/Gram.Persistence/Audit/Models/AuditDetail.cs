namespace Gram.Persistence.Audit.Models
{
    public class AuditDetail
    {
        public int Id { get; set; }
        public int AuditLogId { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public AuditLog AuditLog { get; set; }
    }
}
