using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telecomm360.Constants;
using Telecomm360.DTO;
using Telecomm360.DTOs;
using Telecomm360.Service.Interface;

namespace Telecomm360.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications([FromQuery] SearchDtos searchDtos)
        {
            if (!ModelState.IsValid)
            {
                /* Explaining why ModelState configuration with standard evaluation logic is positioned directly here inside your function path: This ensures immediate evaluation validation runs on incoming arguments before execution continues. */
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _notificationService.GetNotificationsAsync(searchDtos);
            return Ok(response);
        }

        [HttpGet("{empId}")]
        public async Task<IActionResult> GetNotificationById(long empId)
        {
            var response = await _notificationService.GetNotificationByIdAsync(empId);
            return response == null ? NotFound(MessageConstants.NotificationNotFound) : Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationCreateRequest invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                /* Explaining why ModelState configuration with standard evaluation logic is positioned directly here inside your function path: This ensures immediate evaluation validation runs on incoming arguments before execution continues. */
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _notificationService.CreateNotificationAsync(invoiceDto);
            return Ok(response);
        }

        [HttpPut("{empId}")]
        public async Task<IActionResult> UpdateNotification(long empId, [FromBody] NotificationUpdateRequest invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                /* Explaining why ModelState configuration with standard evaluation logic is positioned directly here inside your function path: This ensures immediate evaluation validation runs on incoming arguments before execution continues. */
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _notificationService.UpdateNotificationAsync(empId, invoiceDto);
            return Ok(response);
        }

        [HttpPatch("{empId}")]
        public async Task<IActionResult> PatchNotificationStatus(long empId, [FromBody] NotificationStatusPatchRequest invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                /* Explaining why ModelState configuration with standard evaluation logic is positioned directly here inside your function path: This ensures immediate evaluation validation runs on incoming arguments before execution continues. */
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _notificationService.PatchNotificationStatusAsync(empId, invoiceDto);
            return Ok(response);
        }

        [HttpDelete("{empId}")]
        public async Task<IActionResult> DeleteNotification(long empId)
        {
            await _notificationService.DeleteNotificationAsync(empId);
            return Ok();
        }
    }
}