using Microsoft.AspNetCore.Mvc;
using Telecomm360.Services.Interfaces;
using Telecomm360.DTOs;
using Telecomm360.Enums;
using Telecomm360.Constants;

namespace Telecomm360.Controllers;

[ApiController]
[Route("api/datasets")]
public class AnalyticsDatasetController : ControllerBase
{
    private readonly IAnalyticsService _service;

    public AnalyticsDatasetController(IAnalyticsService service)
    {
        _service = service;
    }

    [HttpGet]
           public IActionResult GetAll([FromQuery] SearchDto search)
    {
        var result = _service.GetAllAnalyticsDatasets(search);
        return Ok(result);
    }

    [HttpPost]
   
        public IActionResult Create(AnalyticsDatasetDto dto)
    {
        if (dto == null)
        {
            return BadRequest(AnalyticsErrorMessages.DatasetCreationFailed);
        }

        var result = _service.CreateAnalyticsDataset(dto);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    
public IActionResult Get(int id)
    {
        if (id <= 0)
        {
            return BadRequest(AnalyticsErrorMessages.InvalidDatasetId);
        }

        var dataset = _service.GetAnalyticsDataset(id);
        
        if (dataset == null)
        {
            // 2. Use the constant here
            return NotFound(new { Message = AnalyticsErrorMessages.DatasetNotFound });
        }

        return Ok(dataset);
    }
    [HttpPut("{id}/refresh")]
   
        public IActionResult Refresh(int id)
    {
        if (id <= 0)
        {
            return BadRequest(AnalyticsErrorMessages.InvalidDatasetId);
        }

        var success = _service.RefreshAnalyticsDataset(id);
        

        return Ok(new { Message = "Dataset refreshed successfully." });
    }
}
