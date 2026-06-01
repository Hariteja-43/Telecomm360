// Path: Telecomm360/Constants/ErrorMessages.cs
namespace Telecomm360.Constants;

public static class DashboardErrorMessages
{
    // Dashboard & Analytics Errors
    public const string DashboardDataFetchFailed = "Unable to retrieve the dashboard analytics data at this time.";
    public const string InvalidDateRange = "The provided start date must be before the end date.";
    public const string UnauthorizedDashboardAccess = "You do not have the required permissions to view telecom analytics metrics.";

    public static object InternalServerError { get; internal set; }
}