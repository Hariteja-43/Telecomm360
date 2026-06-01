using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telecomm360.Constants;
using Telecomm360.DTO;
using Telecomm360.DTOs;
using Telecomm360.Services.Interface;

namespace Telecomm360.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [Authorize(Roles = "Admin")] // Only allow Admin role to manage roles
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles([FromQuery] SearchDto searchDto)
        {
            if (!ModelState.IsValid)
            {
                /* Explaining why ModelState configuration with standard evaluation logic is positioned directly here inside your function path: This ensures immediate evaluation validation runs on incoming arguments before execution continues. */
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _roleService.GetRolesAsync(searchDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateRequest invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                /* Explaining why ModelState configuration with standard evaluation logic is positioned directly here inside your function path: This ensures immediate evaluation validation runs on incoming arguments before execution continues. */
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _roleService.CreateRoleAsync(invoiceDto);
            return Ok(response);
        }

        [HttpPut("{empId}")]
        public async Task<IActionResult> UpdateRole(long empId, [FromBody] RoleUpdateRequest invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                /* Explaining why ModelState configuration with standard evaluation logic is positioned directly here inside your function path: This ensures immediate evaluation validation runs on incoming arguments before execution continues. */
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _roleService.UpdateRoleAsync(empId, invoiceDto);
            return Ok(response);
        }

        [HttpDelete("{empId}")]
        public async Task<IActionResult> DeleteRole(long empId)
        {
            await _roleService.DeleteRoleAsync(empId);
            return Ok();
        }

        [HttpPatch("{empId}/status")]
        public async Task<IActionResult> PatchRoleStatus(long empId, [FromBody] RoleStatusPatchRequest invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                /* Explaining why ModelState configuration with standard evaluation logic is positioned directly here inside your function path: This ensures immediate evaluation validation runs on incoming arguments before execution continues. */
                return BadRequest(MessageConstants.InvalidModel);
            }
            var response = await _roleService.PatchRoleStatusAsync(empId, invoiceDto);
            return Ok(response);
        }
    }
}