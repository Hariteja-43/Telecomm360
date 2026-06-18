namespace Telecomm360.DTOs;

public class UsageRecordDto
{
    public int UsageRecordId { get; set; }
    public int SubscriberID { get; set; } = 0;
    public string ServiceType { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
