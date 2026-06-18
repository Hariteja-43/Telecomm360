namespace Telecom360.DTO.Retention
{
    public class RetentionPolicyResponseDto
    {
        public int PolicyID { get; set; }
        public required string DataType { get; set; }
        public int RetentionPeriod { get; set; }
        public DateTime AppliedFrom { get; set; }
    }
}