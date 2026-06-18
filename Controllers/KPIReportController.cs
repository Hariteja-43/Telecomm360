using Microsoft.AspNetCore.Mvc;
using Telecomm360.DTOs;
using Telecomm360.Service.Interfaces;
using Telecomm360.Constants;
using Telecomm360.DTO;

namespace Telecomm360.Controllers;

[ApiController]
[Route("api/reports")]
public class KPIReportController : ControllerBase
{
    private readonly IKPIReportService _service;

    //  Constructor Injection
    public KPIReportController(IKPIReportService service)
    {
        _service = service;
    }

    //  GET /api/reports
    [HttpGet]
    public IActionResult GetAll([FromQuery] SearchDto search)
    {
        var reports = _service.GetAllKPIReport(search);
        return Ok(reports);
    }

    //  GET /api/reports/{id}
    [HttpGet("{id}")]
    public IActionResult GetKPIReportById(int KPIReportId)
    {
        if(KPIReportId<=0)
        {
            return BadRequest(new { Message = KPIReportErrorMessages.InvalidReportId });
        }
        var report = _service.GetKPIReportById(KPIReportId);    
        if (report == null)
        {
            return NotFound(new {Message = KPIReportErrorMessages.ReportNotFound(KPIReportId)});
        }

        return Ok(report);
    }

    //  GET /api/reports/scope/{scope}
    [HttpGet("scope/{scope}")]
    public IActionResult GetKPIReportByScope(string KPIReportscope)
    {
        if (String.IsNullOrWhiteSpace(KPIReportscope))
        {
            return BadRequest(new { Message = KPIReportErrorMessages.InvalidReportScope });
        }
        var reports = _service.GetKPIReportByScope(KPIReportscope);
        return Ok(reports);
    }

    //  POST /api/reports
    [HttpPost]
    public IActionResult Create(KPIReportDto dto)
    {
        if (dto == null)
            return BadRequest(new { Message = KPIReportErrorMessages.InvalidReportData }    );

        var createdReport = _service.CreateKPIReport(dto);
        return CreatedAtAction(nameof(GetKPIReportById), new { id = createdReport.KPIReportId }, createdReport);
    }
    

    //  DELETE /api/reports/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int KPIReportId)
    {
        if (KPIReportId <= 0)
        {
            return BadRequest(new { Message = KPIReportErrorMessages.InvalidReportId });
        }

        var existing = _service.GetKPIReportById(KPIReportId);

        if (existing == null)
        {
            return NotFound(new { Message = KPIReportErrorMessages.ReportNotFound(KPIReportId) });
        }

        _service.DeleteKPIReport(KPIReportId);
        return Ok(new { Message = $"Report {KPIReportId} deleted successfully" });
    }

    //  GET /api/reports/{id}/export
    [HttpGet("{id}/export")]
    public IActionResult Export(int KPIReportId)
    {
        if (KPIReportId <= 0)
        {
            return BadRequest(new { Message = KPIReportErrorMessages.InvalidReportId });
        }

        var report = _service.GetKPIReportById(KPIReportId);

        if (report == null)
        {
            return NotFound(new { Message = KPIReportErrorMessages.ReportNotFound(KPIReportId) });
        }

        return Ok(report);
    }
}