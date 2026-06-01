using Telecom360.Services.Interface;
using Telecom360.Repository.Interface;
using Telecom360.DTO.Compliance;
using Telecom360.Models;

namespace Telecom360.Services.Implementation
{
    public class ComplianceReportService : IComplianceReportService
    {
        private readonly IComplianceReportRepository _repo;

        public ComplianceReportService(IComplianceReportRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ComplianceReportResponseDto>> GetAllComplianceReports()
        {
            var reports = await _repo.GetAllComplianceReports();

            return reports.Select(r => new ComplianceReportResponseDto
            {
                ReportId = r.ReportId,
                Type = r.Type,
                Scope = r.Scope,
                GeneratedDate = r.GeneratedDate
            });
        }

        public async Task<ComplianceReportResponseDto>  GetComplianceReportById(int complianceReportId)
        {
            var report = await _repo. GetComplianceReportById(complianceReportId);
            if (report == null) return null;

            return new ComplianceReportResponseDto
            {
                ReportId = report.ReportId,
                Type = report.Type,
                Scope = report.Scope,
                GeneratedDate = report.GeneratedDate
            };
        }

        public async Task<ComplianceReportResponseDto> CreateComplianceReport(GenerateComplianceReportRequestDto request)
        {
            var report = new ComplianceReport
            {
                // ReportId = request.ReportId,
                Type = request.Type,
                Scope = request.Scope,
                GeneratedDate = DateTime.UtcNow
            };

            var created = await _repo.CreateComplianceReport(report);

            return new ComplianceReportResponseDto
            {
                ReportId = created.ReportId,
                Type = created.Type,
                Scope = created.Scope,
                GeneratedDate = created.GeneratedDate
            };
        }
    }
}