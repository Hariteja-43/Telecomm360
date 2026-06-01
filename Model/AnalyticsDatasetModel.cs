using System.ComponentModel.DataAnnotations;
namespace Telecomm360.Models;
public class AnalyticsDataset
{
    [Key]
    public int DatasetID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Schema { get; set; } = string.Empty;
    public DateTime LastRefreshed { get; set; } = DateTime.UtcNow;
}
