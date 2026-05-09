using AutoMapper;
using CRM.WebApp.DTOs.LoggingDto;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.LogEntry;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class SystemLogsController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public SystemLogsController(ILogService logService, IMapper mapper)
        {
            _logService = logService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(LogFilterDto filter)
        {
            var result = await _logService.GetPaginatedLogsAsync(filter);
            var summary = await _logService.GetLogSummaryAsync();

            var model = new LogIndexViewModel
            {
                Logs = result,
                PageNumber = filter.Page,
                PageSize = filter.PageSize,
                TotalPages = result.TotalPages,
                LevelFilter = filter.Level,
                SearchFilter = filter.Search,
                LogSummary = summary
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var log = await _logService.GetLogByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            return View(log);
        }

        [HttpPost]
        public async Task<IActionResult> ClearLogs(int daysToKeep = 30)
        {
            await _logService.ClearLogsAsync(daysToKeep);
            TempData["Message"] = $"Logs older than {daysToKeep} days have been cleared.";
            return RedirectToAction(nameof(Index));
        }
    }
}
