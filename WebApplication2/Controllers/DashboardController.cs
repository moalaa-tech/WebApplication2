using CRM.WebApp.Services.SalesManagement;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers
{
    public class DashboardController : Controller
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
            return View();
        }
    }

}
