using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telecomm360.DTOs;
using Telecomm360.Service.Interface;

namespace Telecomm360.Controllers;

[ApiController]
[Route("api/usage")]
[Authorize(Roles = "BillingSpecialist")]
public class UsageRecordController : ControllerBase
{
    private readonly IUsageRecordService _usageRecordService;

    public UsageRecordController(IUsageRecordService usageRecordService)
    {
        _usageRecordService = usageRecordService;
    }

    [HttpGet]
    public ActionResult<List<UsageRecordDto>> GetAllUsageRecords()
    {
        var records = _usageRecordService.GetAllUsageRecord(new UsageRecordDto());
        return Ok(records);
    }

    [HttpGet("{id}")]
    public ActionResult<UsageRecordDto> GetUsageRecordById(int id)
    {
        var record = _usageRecordService.GetUsageRecordById(new UsageRecordDto { UsageRecordId = id });
        if (record == null)
        {
            return NotFound();
        }
        return Ok(record);
    }

    [HttpGet("subscriber/{subscriberId}")]
    public ActionResult<List<UsageRecordDto>> GetUsageRecordsBySubscriber(int subscriberId)
    {
        var records = _usageRecordService.GetUsageRecordBySubscriber(new UsageRecordDto { SubscriberID = subscriberId });
        return Ok(records);
    }

    [HttpPost]
    public ActionResult<UsageRecordDto> CreateUsageRecord(UsageRecordDto usageRecordDto)
    {
        var createdRecord = _usageRecordService.CreateUsageRecord(usageRecordDto);
        return CreatedAtAction(nameof(GetUsageRecordById), new { id = createdRecord.UsageRecordId}, createdRecord);
    }
}

