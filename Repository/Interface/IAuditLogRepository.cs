using System.Collections.Generic;
using System.Threading.Tasks;
using Telecomm360.DTO;
using Telecomm360.Model;

namespace Telecomm360.Repositories.Interface
{
    public interface IAuditLogRepository
    {
        Task<IEnumerable<AuditLog>> GetAllAuditLogsAsync(AuditLogSearchDto searchDto);
        Task AddAuditLogAsync(AuditLog auditLog);
    }
}