using Microsoft.AspNetCore.Mvc;
using Telecom360.Services.Interface;
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
        public async Task<IActionResult> GetComplianceReportById(int policyID)
        {
            // policyID less than zero check send bad request
            try
            {
                var report = await _service.GetComplianceReportById(policyID);
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