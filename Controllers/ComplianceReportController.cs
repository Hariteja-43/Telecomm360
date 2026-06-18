using Microsoft.AspNetCore.Mvc;
using Telecom360.Service.Interface;
using Telecom360.DTO.Compliance;
using Telecom360.Constant;

namespace Telecom360.Controllers
{
    [ApiController]
    [Route("compliance-reports")]
    public class ComplianceReportController : ControllerBase
    { 
        private readonly IComplianceReportService _service;

        public ComplianceReportController(IComplianceReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComplianceReports()
        {
                return Ok(await _service.GetAllComplianceReports());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComplianceReportById(int id)
        {
            if (id <= 0) return BadRequest(ErrorMessages.INVALID_ID);
            try
            {
                var report = await _service.GetComplianceReportById(id);
                return report == null ? NotFound(ErrorMessages.NOT_FOUND) : Ok(report);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateComplianceReport(GenerateComplianceReportRequestDto request)
        {
            try
            {
                return Ok(await _service.CreateComplianceReport(request));
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }
    }
}