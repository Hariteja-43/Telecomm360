using Telecomm360.DTOs;

namespace Telecomm360.Services.Interfaces;

public interface IKPIReportService
{
    List<KPIReportDto> GetAllKPIReport(SearchDto search);
    KPIReportDto GetKPIReportById(int KPIReportid);
    List<KPIReportDto> GetKPIReportByScope(string KPIReportscope);
    KPIReportDto CreateKPIReport(KPIReportDto KPIReportdto);
    void DeleteKPIReport(int KPIReportid);
}
