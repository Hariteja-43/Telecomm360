using Microsoft.AspNetCore.Mvc;
using Telecomm360.DTO;
using Telecomm360.Service.Interface;
using Telecomm360.Constants;

namespace Telecomm360.Controllers
{
    [ApiController]
    [Route("api/subscribers")]
    public class SubscribersController : ControllerBase
    {
        private readonly ISubscriberService _service;

        public SubscribersController(ISubscriberService service)
        {
            _service = service;
        }

        // CREATE SUBSCRIBER
        [HttpPost]
        public async Task<IActionResult> CreateSubscriber([FromBody] CreateSubscriberRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralConstants.InvalidInput);
            }

            var result = await _service.CreateSubscriberAsync(dto);

            return Ok(new
            {
                data = result
            });
        }

        // GET ALL SUBSCRIBERS
        [HttpGet]
        public async Task<IActionResult> GetAllSubscriber()
        {
            var result = await _service.GetAllSubscribersAsync();

            return Ok(new
            {
                data = result
            });
        }

        // GET SUBSCRIBER BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubscriberById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(GeneralConstants.InvalidInput);
            }

            var result = await _service.GetSubscriberByIdAsync(id);

            if (result == null)
            {
                return NotFound(SubscriberConstants.NotFound);
            }

            return Ok(new
            {
                data = result
            });
        }

        // UPDATE SUBSCRIBER
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubscriberById(int id, [FromBody] UpdateSubscriberRequestDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(GeneralConstants.InvalidInput);
            }

            var updated = await _service.UpdateSubscriberAsync(id, dto);

            if (updated == null)
            {
                return NotFound(SubscriberConstants.NotFound);
            }

            return Ok(new
            {
                data = updated
            });
        }

        // DELETE SUBSCRIBER
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriberById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(GeneralConstants.InvalidInput);
            }

            var isDeleted = await _service.DeleteSubscriberAsync(id);

            if (!isDeleted)
            {
                return NotFound(SubscriberConstants.NotFound);
            }

            return Ok();
        }
    }
}