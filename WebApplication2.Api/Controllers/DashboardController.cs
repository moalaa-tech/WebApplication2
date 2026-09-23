using CRM.WebApp.Services.SalesManagement;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IOpportunityService OpportunityService;

        public DashboardController(IOpportunityService _OpportunityService)
        {
            OpportunityService = _OpportunityService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //var data = _context.Opportunities
            //    .GroupBy(o => o.Stage)
            //    .Select(g => new { Stage = g.Key.ToString(), Count = g.Count() })
            //    .ToList();

            //ViewBag.ChartLabels = JsonConvert.SerializeObject(data.Select(d => d.Stage));
            //ViewBag.ChartData = JsonConvert.SerializeObject(data.Select(d => d.Count));
            return Ok();
        }
    }
}