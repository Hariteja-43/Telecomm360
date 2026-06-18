using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telecomm360.Constants;
using Telecomm360.DTO;
using Telecomm360.Service.Interface;

namespace Telecomm360.Controllers
{
    [ApiController]
    [Route("api/alarms")]
    [Authorize(Roles = "Admin,User")] // Allow both Admin and User roles to access alarms
    public class AlarmController : ControllerBase
    {
        private readonly IAlarmService _alarmService;

        public AlarmController(IAlarmService alarmService)
        {
            _alarmService = alarmService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")] // Allow both Admin and User roles to access alarms
        public async Task<IActionResult> GetAlarms([FromQuery] SearchDtos searchDtos)
        {
            if (!ModelState.IsValid)
            {
               
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _alarmService.GetAlarmsAsync(searchDtos);
            return Ok(response);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetAlarmsSummary()
        {
            var response = await _alarmService.GetAlarmsSummaryAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlarm([FromBody] AlarmCreateRequest invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _alarmService.CreateAlarmAsync(invoiceDto);
            return Ok(response);
        }
    }
}