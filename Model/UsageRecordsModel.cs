using System.ComponentModel.DataAnnotations;

namespace Telecomm360.Models;

public class UsageRecord
{
    
    [Key]
    public int RecordID { get; set; }
    public string SubscriberID { get; set; } = string.Empty;
    public string ServiceType { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    // persisted entity only; storage is handled by repository/service layer
}
