namespace Telecom360.DTO.Retention
{
    public class UpdateRetentionPolicyRequestDto
    {
        public string DataType { get; set; }
        public int RetentionPeriod { get; set; }
    }
}