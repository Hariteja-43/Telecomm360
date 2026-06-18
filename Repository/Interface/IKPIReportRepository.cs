using Telecomm360.DTOs;

namespace Telecomm360.Repository.Interfaces;

public interface IKPIReportRepository
{
    List<KPIReportDto> GetAllKPIReport();
    KPIReportDto? GetKPIReportById(int kpiReportId);
    List<KPIReportDto> GetKPIReportByScope(string kpiReportscope);
    KPIReportDto CreateKPIReport(KPIReportDto kpiReportDto);
    void DeleteKPIReport(int kpiReportId);
}