using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telecomm360.Constants;
using Telecomm360.DTO;
using Telecomm360.Service.Interface;

namespace Telecomm360.Controllers
{
    [ApiController]
    [Route("api/audit-logs")]
    [Authorize(Roles = "Admin")] // Only allow Admin role to access audit logs
    public class AuditController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditLogs([FromQuery] AuditLogSearchDto searchDto)
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _auditLogService.GetAuditLogsAsync(searchDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuditLog([FromBody] AuditLogCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
    
                return BadRequest(MessageConstants.InvalidModel);
            }
            await _auditLogService.CreateAuditLogAsync(request);
            return Ok();
        }
    }
}