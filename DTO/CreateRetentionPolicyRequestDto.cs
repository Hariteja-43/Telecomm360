using System.ComponentModel.DataAnnotations;

namespace Telecom360.DTO.Retention
{
    public class CreateRetentionPolicyRequestDto
    {
        [Required]
        public int PolicyID { get; set; }
        [Required]
        public required string DataType { get; set; }

        [Required]
        public int RetentionPeriod { get; set; }

        [Required]
        public DateTime AppliedFrom { get; set; }
    }
}