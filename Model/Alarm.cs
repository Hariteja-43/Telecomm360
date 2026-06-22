using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telecomm360.Enum;

namespace Telecomm360.Model
{
    public class Alarm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlarmID { get; set; }
        public string Source { get; set; } = string.Empty;
        public SeverityEnum Severity { get; set; }
        public DateTime Timestamp { get; set; }
        public StatusEnum Status { get; set; }
    }
}