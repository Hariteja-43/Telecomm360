using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Telecomm360.Model
{
public class AnalyticsDataset
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AnalyticsDatasetId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Schema { get; set; } = string.Empty;
    public DateTime LastRefreshed { get; set; } = DateTime.UtcNow;
}
}
