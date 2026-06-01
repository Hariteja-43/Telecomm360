using Telecomm360.DTOs;

namespace Telecomm360.Repository.Interfaces;

public interface IKPIReportRepository
{
    List<KPIReportDto> GetAllKPIReport();
    KPIReportDto? GetKPIReportById(int KPIReportid);
    List<KPIReportDto> GetKPIReportByScope(string KPIReportscope);
    KPIReportDto CreateKPIReport(KPIReportDto KPIReportdto);
    void DeleteKPIReport(int KPIReportid);
}