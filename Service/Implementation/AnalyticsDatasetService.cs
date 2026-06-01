using Telecomm360.Models;
using Telecomm360.Repository.Interfaces;
using Telecomm360.Services.Interfaces;
using Telecomm360.DTOs;

namespace Telecomm360.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly IAnalyticsDatasetRepository _repo;

    public AnalyticsService(IAnalyticsDatasetRepository repo)
    {
        _repo = repo;
    }
    public List<AnalyticsDatasetDto> GetAllAnalyticsDatasets(SearchDto search)
        => _repo.GetAllAnalyticsDatasets();

    public AnalyticsDatasetDto? GetAnalyticsDataset(int AnalyticsDatasetid)
        => _repo.GetAnalyticsDatasetById(AnalyticsDatasetid);
    public AnalyticsDatasetDto CreateAnalyticsDataset(AnalyticsDatasetDto AnalyticsDatasetDtodto)
        => _repo.CreateAnalyticsDataset(AnalyticsDatasetDtodto);

    public AnalyticsDatasetDto? RefreshAnalyticsDataset(int AnalyticsDatasetid)
    {
        var data = _repo.GetAnalyticsDatasetById(AnalyticsDatasetid);
        if (data == null) return null;

        data.LastRefreshed = DateTime.UtcNow;
        _repo.UpdateAnalyticsDataset(data);
        return data;
    }
}