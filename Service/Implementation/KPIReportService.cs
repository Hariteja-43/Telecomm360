using System;
using Telecomm360.Models;
using Telecomm360.Repository.Interfaces;
using Telecomm360.Services.Interfaces;
using Telecomm360.DTOs;

namespace Telecomm360.Services;

public class KPIReportService : IKPIReportService
{
    private readonly IKPIReportRepository _repo;

    //  Constructor Injection
    public KPIReportService(IKPIReportRepository repo)
    {
        _repo = repo;
    }

    // Get all reports
    public List<KPIReportDto> GetAllKPIReport(SearchDto search)
    {
        return _repo.GetAllKPIReport();
    }

    //  Get report by ID
    public KPIReportDto? GetKPIReportById(int KPIReportid)
    {
        return _repo.GetKPIReportById(KPIReportid);
    }

    //  Get reports by scope (ARPU, churn, etc.)
    public List<KPIReportDto> GetKPIReportByScope(string KPIReportscope)
    {
        return _repo.GetKPIReportByScope(KPIReportscope);
    }
    //  Create report
    public KPIReportDto CreateKPIReport(KPIReportDto KPIReportdto)
    {
        if (KPIReportdto == null)
            throw new ArgumentNullException(nameof(KPIReportdto));

        KPIReportdto.GeneratedDate = DateTime.UtcNow; // auto-set date
        return _repo.CreateKPIReport(KPIReportdto);
    }
    

    //  Delete report
    public void DeleteKPIReport(int KPIReportid)
    {
        _repo.DeleteKPIReport(KPIReportid);
    }
}

