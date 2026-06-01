namespace Telecomm360.DTOs;

public class KPIReportDto
{
    public int ReportID { get; set; }
    public DateTime GeneratedDate { get; set; }
    public string Metrics { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
}
