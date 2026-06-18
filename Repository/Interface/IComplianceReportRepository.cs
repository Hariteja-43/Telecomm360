using Telecom360.Model;

namespace Telecom360.Repository.Interface
{
    public interface IComplianceReportRepository
    {
        Task<IEnumerable<ComplianceReport>> GetAllComplianceReports();
        Task<ComplianceReport> GetComplianceReportById(int complianceReportId);
        Task<ComplianceReport> CreateComplianceReport(ComplianceReport report);
    }
}