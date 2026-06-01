using System.ComponentModel.DataAnnotations;

namespace Telecomm360.Models;

public class Invoice
{
    [Key]
    public int InvoiceID { get; set; }
    public string CustomerID { get; set; } = string.Empty;
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = "DRAFT"; // DRAFT, FINALIZED, PAID
}
