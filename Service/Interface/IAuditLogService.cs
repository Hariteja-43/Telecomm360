using Telecomm360.DTO;

namespace Telecomm360.Service.Interface
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogResponse>> GetAuditLogsAsync(AuditLogSearchDto searchDto);
        Task CreateAuditLogAsync(AuditLogCreateRequest request);
    }
}