using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telecomm360.DTO;
using Telecomm360.Models;
using Telecomm360.Repositories.Interface;
using Telecomm360.Services.Interface;  

namespace Telecomm360.Services.Implementation
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
            
            // 🛠️ FIXED: Stripped out the formatting to return the exact raw database values
            return logs.Select(l => new AuditLogResponse
            {
                AuditID = l.AuditID, 
                UserID = l.UserID,   
                Action = l.Action,
                Resource = l.Resource,
                Timestamp = l.Timestamp
            });
        }

        public async Task CreateAuditLogAsync(AuditLogCreateRequest request)
        {
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