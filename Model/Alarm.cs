using System;
using Telecomm360.Enum;

namespace Telecomm360.Model
{
    public class Alarm
    {
        public int AlarmID { get; set; }
        public string Source { get; set; } = string.Empty;
        public SeverityEnum Severity { get; set; }
        public DateTime Timestamp { get; set; }
        public StatusEnum Status { get; set; }
    }
}