using System;

namespace Telecomm360.DTO
{
    public class AuditLogCreateRequest
    {
        public int UserId { get; set; }
        public required string ActionPerformed { get; set; }
        public required string TargetResource { get; set; }
    }

    // FIXED: Now perfectly matches your Figma design and Database columns
    public class AuditLogResponse
    {
        public int AuditLogID { get; set; }
        public int UserID { get; set; }
        public string Action { get; set; }
        public required string Resource { get; set; }
        public required DateTime Timestamp { get; set; }
    }
}