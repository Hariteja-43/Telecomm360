// Path: Telecomm360/Constants/ErrorMessages.cs (or InvoiceConstant.cs if using partial)
namespace Telecomm360.Constants;

public static partial class InvoiceErrorMessages
{
    // Invoice Errors
    public const string InvoiceNotFound = "The requested invoice could not be found.";
    public const string InvoiceCreationFailed = "Failed to create the invoice. Please verify your data.";
    public const string InvalidInvoiceData = "The provided invoice data is invalid or empty.";
    public const string InvalidInvoiceId = "The provided invoice ID must be a valid positive integer.";
}