using Microsoft.AspNetCore.Mvc;
using Telecomm360.DTO;
using Telecomm360.Service.Interface;
using Telecomm360.Constants;

namespace Telecomm360.Controllers
{
    [ApiController]
    [Route("api/networkresources")]
    public class NetworkResourcesController : ControllerBase
    {
        private readonly INetworkResourceService _service;

        public NetworkResourcesController(INetworkResourceService service)
        {
            _service = service;
        }

        //  CREATE
        [HttpPost]
        public async Task<IActionResult> CreateNetworkResource([FromBody] CreateNetworkResourceRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(GeneralConstants.InvalidInput);

            var result = await _service.CreateResourceAsync(request);

            return Ok(new
            {
                data = result
            });
        }

        //  GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAllNetworkResource()
        {
            var result = await _service.GetAllResourcesAsync();

            return Ok(new
            {
                data = result
            });
        }

        //  GET BY ID
        [HttpGet("{resourceId}")]
        public async Task<IActionResult> GetNetworkResourceById(int resourceId)
        {
            if (resourceId <= 0)
                return BadRequest(GeneralConstants.InvalidInput);

            var result = await _service.GetResourceByIdAsync(resourceId);

            if (result == null)
                return NotFound(GeneralConstants.NotFound);

            return Ok(new
            {

                data = result
            });
        }

        //  UPDATE
        [HttpPut("{resourceId}")]
        public async Task<IActionResult> UpdateNetworkResourceById(int resourceId, [FromBody] CreateNetworkResourceRequestDto request)
        {
            if (resourceId <= 0)
                return BadRequest(GeneralConstants.InvalidInput);

            var result = await _service.UpdateResourceAsync(resourceId, request);

            if (result == null)
                return NotFound(GeneralConstants.NotFound);

            return Ok(new
            {
                data = result
            });
        }

        // DELETE
        [HttpDelete("{resourceId}")]
        public async Task<IActionResult> DeleteNetworkResourceById(int resourceId)
        {
            if (resourceId <= 0)
                return BadRequest(GeneralConstants.InvalidInput);

            var deleted = await _service.DeleteResourceAsync(resourceId);

            if (!deleted)
                return NotFound(GeneralConstants.NotFound);

            return Ok();
    }
    }
}