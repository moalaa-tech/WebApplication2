using CRM.WebApp.DbContext;
using CRM.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace CRM.WebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
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

        [HttpGet]
        public async Task<IActionResult> landing()
        {
            var controller = HttpContext.Request.RouteValues["controller"]?.ToString();
            var action = HttpContext.Request.RouteValues["action"]?.ToString();


            //var ss = DbContext.Employees.ToList();
            //var mm = DbContext.Departments.ToList();
            _logger.LogInformation("Index action called.");

            return View();
        }


        [HttpGet]
        public IActionResult Index()
        {
            //DbInitializer.Initialize(DbContext);
            var controller = HttpContext.Request.RouteValues["controller"]?.ToString();
            var action = HttpContext.Request.RouteValues["action"]?.ToString();
            _logger.LogInformation("Index action called.");


            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ShowServerToaster()
        {
            TempData["ToasterMessage"] = "This is a server-side toaster message!";
            TempData["ToasterType"] = "success";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(culture)) culture = "en";
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            if (string.IsNullOrWhiteSpace(returnUrl)) returnUrl = Url.Action("Index", "Home")!;
            return LocalRedirect(returnUrl);
        }


        [Route("Home/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.Path = exceptionFeature?.Path;
            ViewBag.Error = exceptionFeature?.Error;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult NotFound(int code)
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.Path = exceptionFeature?.Path;
            ViewBag.Error = exceptionFeature?.Error;
            return View(); // Views/Home/NotFound.cshtml
        }

        [HttpGet]

        public IActionResult ProcurementManagementDashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CustomersManagementDashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SalesManagementDashboard()
        {
            return View();
        }


        [HttpGet]
        public IActionResult EmployeeManagementDashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FinanceManagementDashboard()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GeneralAccountingManagementDashboard()
        {
            return View();
        }


        [HttpGet]
        public IActionResult TemplatesManagementDashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InventoryDashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SettingsManagementDashboard()
        {
            return View();
        }


        [HttpGet]
        public IActionResult RomaDashboard()
        {
            return View();
        }

    }
}
