using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Telecomm360.DTO;
using Telecomm360.Model;

namespace Telecomm360.Controllers
{
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDashboard()
        {
            var customers = new List<Customer>();
            var subscribers = new List<Subscriber>();

            var activeSubscribers = subscribers
                .Where(s => s.Status == Status.Active)
                .ToList();

            int subscriberCount = activeSubscribers.Count;

            decimal totalRevenue = 0;
            decimal arpu = subscriberCount > 0 ? totalRevenue / subscriberCount : 0;

            int totalSubs = subscribers.Count;
            int cancelledSubs = subscribers.Count(s => s.Status == Status.Inactive);

            decimal churnRate = totalSubs > 0
                ? (decimal)cancelledSubs / totalSubs * 100
                : 0;

            var dashboard = new DashboardDto
            {
                Subscribers = subscriberCount,
                ARPU = arpu,
                ChurnRate = churnRate,
                KPIs = $"Subscribers: {subscriberCount}, ARPU: {arpu}, ChurnRate: {churnRate}%"
            };

            return Ok(dashboard);
        }
    }
}