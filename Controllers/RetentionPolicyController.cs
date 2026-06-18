using Microsoft.AspNetCore.Mvc;
using Telecom360.Service.Interface;
using Telecom360.DTO.Retention;
using Telecom360.Constant;

namespace Telecom360.Controllers
{
    [ApiController]
    [Route("retention-policies")]
    public class RetentionPolicyController : ControllerBase
    {
        private readonly IRetentionPolicyService _service;

        public RetentionPolicyController(IRetentionPolicyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRetentionPolicy()
        {
            try
            {
                return Ok(await _service.GetAllRetentionPolicy());
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRetentionPolicyById(int id)
        {
            if (id <= 0) return BadRequest(ErrorMessages.INVALID_ID);
            try
            {
                var policy = await _service.GetRetentionPolicyById(id);
                return policy == null ? NotFound(ErrorMessages.NOT_FOUND) : Ok(policy);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRetentionPolicy([FromBody] CreateRetentionPolicyRequestDto request)
        {
            try
            {
                return Ok(await _service.CreateRetentionPolicy(request));
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRetentionPolicy(int id, UpdateRetentionPolicyRequestDto request)
        {
            if (id <= 0) return BadRequest(ErrorMessages.INVALID_ID);
            try
            {
                    var result = await _service.UpdateRetentionPolicy(id, request);

                return result == null ? NotFound(ErrorMessages.NOT_FOUND) : Ok(result);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }
    }
}