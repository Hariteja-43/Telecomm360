namespace Telecomm360.DTOs;

public class PaymentDto
{
    public int PaymentID { get; set; }
    public int InvoiceID { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Method { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}