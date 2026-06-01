// Path: Telecomm360/Constants/ErrorMessages.cs (or KPIReportConstant.cs if using partial)
namespace Telecomm360.Constants;

public static partial class KPIReportErrorMessages
{
    // KPI Report Errors
    public const string InvalidReportData = "The provided KPI report data is invalid or empty.";
    public const string InvalidReportId = "The provided report ID must be a valid positive integer.";
    public const string InvalidReportScope = "The provided report scope parameter is invalid.";
    public const string ReportDeleteFailed = "An error occurred while attempting to delete the report.";

    // Dynamic Helper Method for ID interpolation
    public static string ReportNotFound(int id) => $"Report with ID {id} not found.";
}