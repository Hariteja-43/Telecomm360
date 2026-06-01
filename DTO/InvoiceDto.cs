namespace Telecomm360.DTOs;

public class InvoiceDto
{
    
    public int? InvoiceID { get; set; }
    public string CustomerID { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Invoiceamount { get; internal set; }
    public object Id { get; internal set; }
}
