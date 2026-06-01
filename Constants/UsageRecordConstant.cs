// Path: Telecomm360/Constants/ErrorMessages.cs (or UsageConstant.cs if using partial)
namespace Telecomm360.Constants;

public static partial class UsageRecordErrorMessages
{
    // Usage Record Errors
    public const string InvalidUsageData = "The provided usage record data is invalid or empty.";
    public const string InvalidUsageRecordId = "The provided usage record ID must be a valid positive integer.";
    public const string InvalidSubscriberId = "The provided subscriber ID is invalid.";

    // Dynamic Helper Method for ID interpolation
    public static string UsageRecordNotFound(int recordId) => $"Usage record with ID {recordId} not found.";
}