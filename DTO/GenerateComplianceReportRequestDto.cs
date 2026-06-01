using System.ComponentModel.DataAnnotations;

namespace Telecom360.DTO.Compliance
{
    public class GenerateComplianceReportRequestDto
    {
        [Required]
        public required string Type { get; set; }

        [Required]
        public required string Scope { get; set; }
    }
}
