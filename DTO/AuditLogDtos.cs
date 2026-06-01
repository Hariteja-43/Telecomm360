using System;

namespace Telecomm360.DTO
{
    public class AuditLogCreateRequest
    {
        public long UserId { get; set; }
        public required string ActionPerformed { get; set; }
        public required string TargetResource { get; set; }
    }

    // 🛠️ FIXED: Now perfectly matches your Figma design and Database columns
    public class AuditLogResponse
    {
        public long AuditID { get; set; }
        public long UserID { get; set; }
        public required string Action { get; set; }
        public required string Resource { get; set; }
        public required DateTime Timestamp { get; set; }
    }
}