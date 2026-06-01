

using Telecomm360.Data;
using Telecomm360.Models;
using Telecomm360.Repository.Interfaces;
using Telecomm360.DTOs;

namespace Telecomm360.Repository;

public class AnalyticsDatasetRepository : IAnalyticsDatasetRepository
{
    private readonly AppDbContext _context;

    public AnalyticsDatasetRepository(AppDbContext context)
    {
        _context = context;
    }

    private AnalyticsDatasetDto ToDto(AnalyticsDataset e) => new AnalyticsDatasetDto
    {
        DatasetID = e.DatasetID,
        LastRefreshed = e.LastRefreshed,
        Name = e.Name,
        Schema = e.Schema
    };

    private AnalyticsDataset ToEntity(AnalyticsDatasetDto d) => new AnalyticsDataset
    {
        DatasetID = d.DatasetID,
        LastRefreshed = d.LastRefreshed,
        Name = d.Name,
        Schema = d.Schema
    };

    public List<AnalyticsDatasetDto> GetAllAnalyticsDatasets()
        => _context.Set<AnalyticsDataset>().ToList().Select(e => ToDto(e)).ToList();

    public AnalyticsDatasetDto? GetAnalyticsDatasetById(int AnalyticsDatasetid)
        => _context.Set<AnalyticsDataset>().FirstOrDefault(x => x.DatasetID == AnalyticsDatasetid) is AnalyticsDataset e ? ToDto(e) : null;

    public AnalyticsDatasetDto CreateAnalyticsDataset(AnalyticsDatasetDto AnalyticsDatasetDtodto)
    {
        var entity = ToEntity(AnalyticsDatasetDtodto);
        _context.Set<AnalyticsDataset>().Add(entity);
        _context.SaveChanges();
        return ToDto(entity);
    }

    public void UpdateAnalyticsDataset(AnalyticsDatasetDto AnalyticsDatasetdto)
    {
        var entity = ToEntity(AnalyticsDatasetdto);
        _context.Set<AnalyticsDataset>().Update(entity);
        _context.SaveChanges();
    }

    public void DeleteAnalyticsDataset(int AnalyticsDatasetid)
    {
        var data = _context.Set<AnalyticsDataset>().FirstOrDefault(x => x.DatasetID == AnalyticsDatasetid);
        if (data != null)
        {
            _context.Remove(data);
            _context.SaveChanges();
        }
    }
}