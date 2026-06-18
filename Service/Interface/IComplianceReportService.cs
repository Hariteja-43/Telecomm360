using Telecom360.DTO.Compliance;

namespace Telecom360.Service.Interface
{
    public interface IComplianceReportService
    {
        Task<IEnumerable<ComplianceReportResponseDto>> GetAllComplianceReports();
        Task<ComplianceReportResponseDto> GetComplianceReportById(int complianceReportId);
        Task<ComplianceReportResponseDto> CreateComplianceReport(GenerateComplianceReportRequestDto request);
    }
}