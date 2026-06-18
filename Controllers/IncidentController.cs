using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telecomm360.Constants;
using Telecomm360.DTO;
using Telecomm360.Service.Interface;

namespace Telecomm360.Controllers
{
    [ApiController]
    [Route("api/incidents")]
    [Authorize(Roles = "Admin,User")] // Allow both Admin and User roles to access incidents
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentsController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetIncidents([FromQuery] SearchDtos searchDtos)
        {
            if (!ModelState.IsValid)
            {
                /* Explaining why ModelState configuration with standard evaluation logic is positioned directly here inside your function path: This ensures immediate evaluation validation runs on incoming arguments before execution continues. */
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _incidentService.GetIncidentsAsync(searchDtos);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentCreateRequest invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                /* Explaining why ModelState configuration with standard evaluation logic is positioned directly here inside your function path: This ensures immediate evaluation validation runs on incoming arguments before execution continues. */
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _incidentService.CreateIncidentAsync(invoiceDto);
            return Ok(response);
        }

        [HttpPatch("{empId}")]
        public async Task<IActionResult> PatchIncident(long empId, [FromBody] IncidentPatchRequest invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                /* Explaining why ModelState configuration with standard evaluation logic is positioned directly here inside your function path: This ensures immediate evaluation validation runs on incoming arguments before execution continues. */
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _incidentService.PatchIncidentAsync(empId, invoiceDto);
            return Ok(response);
        }
    }
}