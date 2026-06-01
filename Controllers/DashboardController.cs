using Microsoft.AspNetCore.Mvc;

namespace Telecomm360.Controllers;

[ApiController]
[Route("api/analytics")]
public class DashboardController : ControllerBase
{
    [HttpGet("dashboard")]
    public IActionResult GetDashboard()
    {
        try
        {
            
        
        var data = new
        {
            Subscribers = 1000,
            ARPU = 250,
            ChurnRate = 5,
            KPIs = "Sample KPI metrics"
        };
        if (data == null)
        {
            return NotFound(new { Message = Constants.DashboardErrorMessages.DashboardDataFetchFailed });
        }

        return Ok(data);
    }
    catch (Exception)
    {
        return StatusCode(500, new { Message = Constants.DashboardErrorMessages.InternalServerError });
    }
    }
}