using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telecomm360.Enum;

namespace Telecomm360.Model
{
    public class Incident
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IncidentID { get; set; }
        public int AlarmID { get; set; }
        public Alarm Alarm { get; set; }
        public int AssignedTo { get; set; }
        public PriorityEnum Priority { get; set; }
        public StatusEnum Status { get; set; }
        public string ResolutionNotes { get; set; } = string.Empty;
    }
}