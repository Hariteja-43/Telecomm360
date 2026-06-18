namespace Telecomm360.DTOs;

public class KPIReportDto
{
    public int KPIReportId { get; set; }
    public DateTime GeneratedDate { get; set; }
    public string Metrics { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
}
