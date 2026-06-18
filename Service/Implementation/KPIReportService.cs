using System;
using Telecomm360.Model;
using Telecomm360.Repository.Interfaces;
using Telecomm360.Service.Interfaces;
using Telecomm360.DTOs;
using Telecomm360.DTO;

namespace Telecomm360.Service.Implementation
{

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
    public KPIReportDto? GetKPIReportById(int kpiReportId)
    {
        return _repo.GetKPIReportById(kpiReportId);
    }

    //  Get reports by scope (ARPU, churn, etc.)
    public List<KPIReportDto> GetKPIReportByScope(string kpiReportscope)
    {
        return _repo.GetKPIReportByScope(kpiReportscope);
    }
    //  Create report
    public KPIReportDto CreateKPIReport(KPIReportDto kpiReportDto)
    {
        if (kpiReportDto == null)
            throw new ArgumentNullException(nameof(kpiReportDto));

        kpiReportDto.GeneratedDate = DateTime.UtcNow; // auto-set date
        return _repo.CreateKPIReport(kpiReportDto);
    }
    

    //  Delete report
    public void DeleteKPIReport(int kpiReportId)
    {
        _repo.DeleteKPIReport(kpiReportId);
    }
}
}

