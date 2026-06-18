using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telecom360.Model
{
    public class ComplianceReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplianceReportId { get; set; }
        public required string Type { get; set; }
        public required string Scope { get; set; }
        public DateTime GeneratedDate { get; set; }
    }
}