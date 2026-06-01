using System.Collections.Generic;
using System.Threading.Tasks;
using Telecomm360.DTO;

namespace Telecomm360.Services.Interface
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogResponse>> GetAuditLogsAsync(AuditLogSearchDto searchDto);
        Task CreateAuditLogAsync(AuditLogCreateRequest request);
    }
}