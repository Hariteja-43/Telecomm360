using System.ComponentModel.DataAnnotations;

namespace Telecomm360.Model
{   

public class UsageRecord
{
    
    [Key]
    public int UsageRecordId { get; set; }
    public int SubscriberID { get; set; } = 0;
    public string ServiceType { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    // persisted entity only; storage is handled by repository/service layer
}
}
