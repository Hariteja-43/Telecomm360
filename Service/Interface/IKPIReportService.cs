using Telecomm360.DTO;
using Telecomm360.DTOs;

namespace Telecomm360.Service.Interfaces
{

public interface IKPIReportService
{
    List<KPIReportDto> GetAllKPIReport(SearchDto search);
    KPIReportDto GetKPIReportById(int kpiReportId);
    List<KPIReportDto> GetKPIReportByScope(string kpiReportscope);
    KPIReportDto CreateKPIReport(KPIReportDto kpiReportDto);
    void DeleteKPIReport(int kpiReportId);
}
}