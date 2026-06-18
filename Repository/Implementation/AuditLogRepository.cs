using System.Threading.Tasks;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.DTO;
using Telecomm360.Model;
using Telecomm360.Repositories.Interface;


namespace Telecomm360.Repository.Implementation
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(AppDbContext context)
        {
            _context = context;
        }

        // ✅ UPDATED: ADVANCED FILTER + PAGINATION
        public async Task<IEnumerable<AuditLog>> GetAllAuditLogsAsync(AuditLogSearchDto searchDto)
{
    var query = _context.AuditLogs.AsQueryable();

    if (searchDto.UserId.HasValue)
    {
        query = query.Where(x => x.UserID == searchDto.UserId.Value);
    }

    if (!string.IsNullOrWhiteSpace(searchDto.Action))
    {
        query = query.Where(x => x.Action.Contains(searchDto.Action));
    }

    if (searchDto.FromDate.HasValue)
    {
        query = query.Where(x => x.Timestamp >= searchDto.FromDate.Value);
    }

    if (searchDto.ToDate.HasValue)
    {
        query = query.Where(x => x.Timestamp <= searchDto.ToDate.Value);
    }

    return await query
        .OrderByDescending(x => x.Timestamp)
        .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
        .Take(searchDto.PageSize)
        .ToListAsync();
}

        // ✅ CREATE AUDIT LOG
        public async Task AddAuditLogAsync(AuditLog auditLog)
        {
            await _context.AuditLogs.AddAsync(auditLog);
            await _context.SaveChangesAsync();
        }
    }
}