using Telecom360.Model;

namespace Telecomm360.Model
{
    public class AuditLog
    {
        public int AuditLogID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public string Resource { get; set; }
        public DateTime Timestamp { get; set; }
        public string Action { get; internal set; }
    }
}