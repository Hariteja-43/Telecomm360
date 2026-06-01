using System.ComponentModel.DataAnnotations;
namespace Telecomm360.Models;
public class KPIReport
{
    [Key]


    public int ReportID { get; set; }
    public string Scope { get; set; } = string.Empty;
    public string Metrics { get; set; } = string.Empty;
    public DateTime GeneratedDate { get; set; } = DateTime.UtcNow;
}