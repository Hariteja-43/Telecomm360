
using Telecomm360.Data;
using Telecomm360.Models;
using Telecomm360.Repository.Interfaces;
using Telecomm360.DTOs;

namespace Telecomm360.Repository;

public class KPIReportRepository : IKPIReportRepository
{
    private readonly AppDbContext _context;

    public KPIReportRepository(AppDbContext context)
    {
        _context = context;
    }

    // Map entity -> dto
    private KPIReportDto ToDto(KPIReport r) => new KPIReportDto
    {
        ReportID = r.ReportID,
        GeneratedDate = r.GeneratedDate,
        Metrics = r.Metrics,
        Scope = r.Scope
    };

    // Map dto -> entity
    private KPIReport ToEntity(KPIReportDto d) => new KPIReport
    {
        ReportID = d.ReportID,
        GeneratedDate = d.GeneratedDate,
        Metrics = d.Metrics,
        Scope = d.Scope
    };

    public List<KPIReportDto> GetAllKPIReport()
        => _context.Set<KPIReport>().ToList().Select(r => ToDto(r)).ToList();

    public KPIReportDto? GetKPIReportById(int KPIReportid)
        => _context.Set<KPIReport>().FirstOrDefault(x => x.ReportID == KPIReportid) is KPIReport r ? ToDto(r) : null;

    public List<KPIReportDto> GetKPIReportByScope(string KPIReportscope)
        => _context.Set<KPIReport>()
            .Where(x => x.Scope == KPIReportscope)
            .ToList()
            .Select(r => ToDto(r))
            .ToList();

    public KPIReportDto CreateKPIReport(KPIReportDto KPIReportdto)
    {
        var entity = ToEntity(KPIReportdto);
        _context.Set<KPIReport>().Add(entity);
        _context.SaveChanges();
        return ToDto(entity);
    }

    public void DeleteKPIReport(int KPIReportid)
    {
        var data = _context.Set<KPIReport>().FirstOrDefault(x => x.ReportID == KPIReportid);
        if (data != null)
        {
            _context.Remove(data);
            _context.SaveChanges();
        }
    }

    public void UpdateKPIReport(KPIReportDto KPIReportdto)
    {
        var entity = ToEntity(KPIReportdto);
        _context.Set<KPIReport>().Update(entity);
        _context.SaveChanges();
    }
}
