using Microsoft.AspNetCore.Mvc;
using Telecomm360.DTO;
using Telecomm360.Service.Interface;
using Telecomm360.Constants;

namespace Telecomm360.Controllers
{
    [ApiController]
    [Route("api/provisioningtasks")]
    public class ProvisioningTasksController : ControllerBase
    {
        private readonly IProvisioningTaskService _service;

        public ProvisioningTasksController(IProvisioningTaskService service)
        {
            _service = service;
        }

        //  CREATE
        [HttpPost]
        public async Task<IActionResult> CreateProvisioningTasks([FromBody] CreateProvisioningTaskRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(GeneralConstants.InvalidInput);

            var result = await _service.CreateTaskAsync(dto);

            return Ok(new
            {
                data = result
            });
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAllProvisioningTasks()
        {
            var result = await _service.GetAllTasksAsync();

            return Ok(new
            {
                data = result
            });
        }

        //  GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProvisioningTasksById(int id)
        {
            if (id <= 0)
                return BadRequest(GeneralConstants.InvalidInput);

            var result = await _service.GetTaskByIdAsync(id);

            if (result == null)
                return NotFound(ProvisioningTaskConstants.NotFound);

            return Ok(new
            {
                data = result
            });
        }

        //  UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvisioningTasks(int id, [FromBody] CreateProvisioningTaskRequestDto dto)
        {
            if (id <= 0)
                return BadRequest(GeneralConstants.InvalidInput);

            var result = await _service.UpdateTaskAsync(id, dto);

            if (result == null)
                return NotFound(ProvisioningTaskConstants.NotFound);

            return Ok(new
            {
                data = result
            });
        }

        //  DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvisioningTasks(int id)
        {
            if (id <= 0)
                return BadRequest(GeneralConstants.InvalidInput);

            var isDeleted = await _service.DeleteTaskAsync(id);

            if (!isDeleted)
                return NotFound(ProvisioningTaskConstants.NotFound);

            return Ok();
        }
    }
}