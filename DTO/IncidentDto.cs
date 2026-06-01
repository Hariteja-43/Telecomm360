namespace Telecomm360.DTO
{
    public class IncidentCreateRequest
    {
        public string TargetAlarmId { get; set; } = string.Empty;
        public string IncidentPriority { get; set; } = string.Empty;
    }

    public class IncidentPatchRequest
    {
        public string UpdatedStatus { get; set; } = string.Empty;
        public string ResolutionDetails { get; set; } = string.Empty;
    }

    public class IncidentResponse
    {
        public string DisplayId { get; set; } = string.Empty;
        public string AssignedEngineer { get; set; } = string.Empty;
        public string IncidentPriority { get; set; } = string.Empty;
        public string CurrentStatus { get; set; } = string.Empty;
        public string ResolutionDetails { get; set; } = string.Empty;
    }
}