// Path: Telecomm360/Constants/ErrorMessages.cs (or PaymentConstant.cs if using partial)
namespace Telecomm360.Constants;

public static partial class PaymentErrorMessages
{
    // Payment Errors
    public const string InvalidPaymentPayload = "The provided payment payload is invalid or empty.";
    public const string InvalidPaymentId = "The provided payment ID must be a valid positive integer.";
    public const string PaymentReconciliationFailed = "Unable to process reconciliation for this payment.";

    // Dynamic Helper Method for ID interpolation
    public static string PaymentNotFound(int id) => $"Payment record with ID {id} could not be found.";
}