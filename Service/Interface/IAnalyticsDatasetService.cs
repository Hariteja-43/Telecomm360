using Telecomm360.DTOs;

namespace Telecomm360.Services.Interfaces;

public interface IAnalyticsService
{
    List<AnalyticsDatasetDto> GetAllAnalyticsDatasets(SearchDto search);
    AnalyticsDatasetDto? GetAnalyticsDataset(int AnalyticsDatasetid);
    AnalyticsDatasetDto CreateAnalyticsDataset(AnalyticsDatasetDto AnalyticsDatasetDtodto);
    AnalyticsDatasetDto? RefreshAnalyticsDataset(int AnalyticsDatasetid);
}