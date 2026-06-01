using System.ComponentModel.DataAnnotations;

namespace Telecomm360.Models;

public class Payment
{
    [Key]
    public int PaymentID { get; set; } 
    public int InvoiceID { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Method { get; set; } = "INTERNAL";
    public string Status { get; set; } = "CREATED";
}