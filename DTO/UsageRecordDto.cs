namespace Telecomm360.DTOs;

public class UsageRecordDto
{
    public int RecordID { get; set; }
    public string SubscriberID { get; set; } = string.Empty;
    public string ServiceType { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
