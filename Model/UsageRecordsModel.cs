using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telecomm360.Model
{   

public class UsageRecord
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UsageRecordId { get; set; }
    public int SubscriberID { get; set; } = 0;
    public string ServiceType { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    // persisted entity only; storage is handled by repository/service layer
}
}
