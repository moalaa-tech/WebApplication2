using CRM.WebApp.DbContext;
using CRM.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace WebApplication2.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private ApplicationContext DbContext;

        private readonly ILogger<HomeController> _logger;


        public HomeController(
            ILogger<HomeController> logger,
            ApplicationContext _DbContext
            )
        {
            _logger = logger;
            DbContext = _DbContext;
        }

        [HttpGet("landing")]
        public async Task<IActionResult> landing()
        {
            var controller = HttpContext.Request.RouteValues["controller"]?.ToString();
            var action = HttpContext.Request.RouteValues["action"]?.ToString();


            //var ss = DbContext.Employees.ToList();
            //var mm = DbContext.Departments.ToList();
            _logger.LogInformation("Index action called.");

            return Ok();
        }


        [HttpGet]
        public IActionResult Index()
        {
            //DbInitializer.Initialize(DbContext);
            var controller = HttpContext.Request.RouteValues["controller"]?.ToString();
            var action = HttpContext.Request.RouteValues["action"]?.ToString();
            _logger.LogInformation("Index action called.");


            return Ok();
        }

        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            return Ok();
        }

        [HttpGet("ShowServerToaster")]
        public IActionResult ShowServerToaster()
        {
            return Ok();
        }

        [HttpPost("SetLanguage")]
        public IActionResult SetLanguage([FromQuery] string? culture, [FromQuery] string? returnUrl)
        {
            if (string.IsNullOrWhiteSpace(culture)) culture = "en";
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            if (string.IsNullOrWhiteSpace(returnUrl)) returnUrl = Url.Action("Index", "Home")!;
            return Ok();
        }


        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return Ok(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("NotFound")]
        public IActionResult NotFound([FromQuery] int? code)
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return Ok();
        }

        [HttpGet("ProcurementManagementDashboard")]

        public IActionResult ProcurementManagementDashboard()
        {
            return Ok();
        }

        [HttpGet("CustomersManagementDashboard")]
        public IActionResult CustomersManagementDashboard()
        {
            return Ok();
        }

        [HttpGet("SalesManagementDashboard")]
        public IActionResult SalesManagementDashboard()
        {
            return Ok();
        }


        [HttpGet("EmployeeManagementDashboard")]
        public IActionResult EmployeeManagementDashboard()
        {
            return Ok();
        }

        [HttpGet("FinanceManagementDashboard")]
        public IActionResult FinanceManagementDashboard()
        {
            return Ok();
        }


        [HttpGet("GeneralAccountingManagementDashboard")]
        public IActionResult GeneralAccountingManagementDashboard()
        {
            return Ok();
        }


        [HttpGet("TemplatesManagementDashboard")]
        public IActionResult TemplatesManagementDashboard()
        {
            return Ok();
        }

        [HttpGet("InventoryDashboard")]
        public IActionResult InventoryDashboard()
        {
            return Ok();
        }

        [HttpGet("SettingsManagementDashboard")]
        public IActionResult SettingsManagementDashboard()
        {
            return Ok();
        }


        [HttpGet("RomaDashboard")]
        public IActionResult RomaDashboard()
        {
            return Ok();
        }
    }
}