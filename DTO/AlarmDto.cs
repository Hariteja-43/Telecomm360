using System;

namespace Telecomm360.DTO
{
    public class AlarmCreateRequest
    {
        public string SourceNode { get; set; } = string.Empty;
        public string FaultSeverity { get; set; } = string.Empty;
    }

    public class AlarmResponse
    {
        public int DisplayId { get; set; } = 0;
        public string SourceNode { get; set; } = string.Empty;
        public string FaultSeverity { get; set; } = string.Empty;
        public string CurrentStatus { get; set; } = string.Empty;
        public DateTime FormattedTimestamp { get; set; }
    }

    public class AlarmSummaryResponse
    {
        public int TotalCritical { get; set; }
        public int TotalMajor { get; set; }
        public int TotalMinor { get; set; }
        public int TotalWarning { get; set; }
    }
}