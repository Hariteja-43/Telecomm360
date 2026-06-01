using Telecomm360.Enum;

namespace Telecomm360.Models
{
    public class Incident
    {
        public long IncidentID { get; set; }
        public long AlarmID { get; set; }
        public long AssignedTo { get; set; }
        public PriorityEnum Priority { get; set; }
        public StatusEnum Status { get; set; }
        public string ResolutionNotes { get; set; } = string.Empty;
    }
}