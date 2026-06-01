using System;
using Telecomm360.Enum;

namespace Telecomm360.Models
{
    public class Alarm
    {
        public long AlarmID { get; set; }
        public string Source { get; set; } = string.Empty;
        public SeverityEnum Severity { get; set; }
        public DateTime Timestamp { get; set; }
        public StatusEnum Status { get; set; }
    }
}