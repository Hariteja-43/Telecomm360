using Telecomm360.DTO;
using Telecomm360.Model;
using Telecomm360.Repositories.Interface;
using Telecomm360.Service.Interface;
 
namespace Telecomm360.Service.Implementation
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;
 
        public AuditLogService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }
 
        public async Task<IEnumerable<AuditLogResponse>> GetAuditLogsAsync(AuditLogSearchDto searchDto)
        {
            var logs = await _auditLogRepository.GetAllAuditLogsAsync(searchDto);
           
           
            return logs.Select(l => new AuditLogResponse
            {
                AuditLogID = l.AuditLogID,
                UserID = l.UserID,  
                Action = l.Action,
                Resource = l.Resource,
                Timestamp = l.Timestamp
            });
        }
 
        public async Task CreateAuditLogAsync(AuditLogCreateRequest request)
{
    // Prevent invalid FK error
    if (request.UserId <= 0)
    {
        throw new Exception("Invalid UserID for AuditLog");
    }
 
    var auditLog = new AuditLog
    {
        UserID = request.UserId,
        Action = request.ActionPerformed,
        Resource = request.TargetResource,
        Timestamp = DateTime.UtcNow
    };
 
    await _auditLogRepository.AddAuditLogAsync(auditLog);
}
    }
}
 