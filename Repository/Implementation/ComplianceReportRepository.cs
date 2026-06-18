
using Microsoft.EntityFrameworkCore;
using Telecom360.Model;
using Telecom360.Repository.Interface;
using Telecomm360.Data;

namespace Telecom360.Repository.Implementation
{
    public class ComplianceReportRepository : IComplianceReportRepository
    {
        private readonly AppDbContext _context;

        public ComplianceReportRepository(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET ALL REPORTS
        public async Task<IEnumerable<ComplianceReport>>  GetAllComplianceReports()
        {
            return await _context.ComplianceReports
                .AsNoTracking() 
                .ToListAsync();
        }

        // ✅ GET REPORT BY ID
        public async Task<ComplianceReport>GetComplianceReportById(int complianceReportId)
        {
            return await _context.ComplianceReports
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ComplianceReportId == complianceReportId);
        }

        // ✅ CREATE REPORT
        public async Task<ComplianceReport> CreateComplianceReport(ComplianceReport report)
        {
            await _context.ComplianceReports.AddAsync(report);
            await _context.SaveChangesAsync();

            return report;
        }
    }
}
