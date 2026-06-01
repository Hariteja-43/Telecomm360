namespace Telecomm360.Enums;

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
