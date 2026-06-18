namespace Telecom360.DTO.Compliance
{
    public class ComplianceReportResponseDto
    {
        public int ComplianceReportId { get; set; }
        public required string Type { get; set; }
        public required string Scope { get; set; }
        public DateTime GeneratedDate { get; set; }
    }
}