using System;

namespace Telecomm360.Models
{
    public class AuditLog
    {
        public long AuditID { get; set; }
        public long UserID { get; set; }
        public string Action { get; set; }
        public string Resource { get; set; }
        public DateTime Timestamp { get; set; }
    }
}