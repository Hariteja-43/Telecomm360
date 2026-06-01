using System;
using System.ComponentModel.DataAnnotations;

namespace Telecomm360.DTO
{
    public class AuditLogSearchDto
    {
        public long? UserId { get; set; }
        public string? Action { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}