using AutoMapper;
using CRM.WebApp.DTOs.LoggingDto;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.LogEntry;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class SystemLogsController : ControllerBase
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public SystemLogsController(ILogService logService, IMapper mapper)
        {
            _logService = logService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] LogFilterDto filter)
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

            return Ok(model);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var log = await _logService.GetLogByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            return Ok(log);
        }

        [HttpPost("ClearLogs")]
        public async Task<IActionResult> ClearLogs([FromQuery] int daysToKeep = 30)
        {
            await _logService.ClearLogsAsync(daysToKeep);
            return Ok();
        }
    }
}