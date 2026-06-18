using Telecom360.DTO.Compliance;

namespace Telecom360.Services.Interface
{
    public interface IComplianceReportService
    {
        Task<IEnumerable<ComplianceReportResponseDto>> GetAllComplianceReports();
        Task<ComplianceReportResponseDto> GetComplianceReportById(int policyID);
        Task<ComplianceReportResponseDto> CreateComplianceReport(GenerateComplianceReportRequestDto request);
    }
}