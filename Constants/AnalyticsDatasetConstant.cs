// Path: Telecomm360/Constants/ErrorMessages.cs
namespace Telecomm360.Constants;

public static class AnalyticsErrorMessages
{
    // General Errors
    public const string InternalServerError = "An unexpected error occurred while processing your request.";
    
    // Dataset Errors
    public const string DatasetNotFound = "The requested analytics dataset could not be found.";
    public const string DatasetCreationFailed = "Failed to create the analytics dataset. Please check your inputs.";
    public const string DatasetRefreshFailed = "Unable to refresh the analytics dataset at this time.";
    
    // Validation Errors
    public const string InvalidSearchParameters = "The provided search criteria are invalid.";
    public const string InvalidDatasetId = "The provided dataset ID must be greater than zero.";
}