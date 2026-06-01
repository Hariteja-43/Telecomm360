using Telecomm360.DTOs;

namespace Telecomm360.Repository.Interfaces;

public interface IAnalyticsDatasetRepository
{
    List<AnalyticsDatasetDto> GetAllAnalyticsDatasets();
    AnalyticsDatasetDto? GetAnalyticsDatasetById(int AnalyticsDatasetid);
    AnalyticsDatasetDto CreateAnalyticsDataset(AnalyticsDatasetDto AnalyticsDatasetdto);
    void UpdateAnalyticsDataset(AnalyticsDatasetDto dto);
    void DeleteAnalyticsDataset(int AnalyticsDatasetid);
}