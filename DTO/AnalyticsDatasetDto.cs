namespace Telecomm360.DTOs;

public class AnalyticsDatasetDto
{
    public int DatasetID { get; set; }
    public DateTime LastRefreshed { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Schema { get; set; } = string.Empty;
   
}
