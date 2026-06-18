namespace Telecomm360.Enum
{
/// <summary>
/// Represents the status of an invoice.
/// </summary>
public enum InvoiceStatus
{
    Draft,
    Finalized,
    Paid
}
/// <summary>
/// Represents the status of a payment.
/// </summary>
public enum PaymentStatus
{
    Pending,
    Completed,
    Reconciled,
    Failed
}

/// <summary>
/// Represents the status of a KPI report.
/// </summary>
public enum ReportStatus
{
    Generated,
    Processing,
    Completed,
    Failed
}
public enum StatusEnum
    {
        Open = 1,
        Acknowledged = 2,
        Cleared = 3,
        Closed = 4
    }
}
