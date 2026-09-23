using AutoMapper;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Project;
using CRM.WebApp.Services.ProjectManagment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication2.Api.Controllers.ProjectManagament
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TimeEntriesController : ControllerBase
    {
        private readonly ITimeEntryService _timeEntryService;
        private readonly IJobPhaseService _phaseService;
        private readonly IMapper _mapper;

        public TimeEntriesController(
            ITimeEntryService timeEntryService,
            IJobPhaseService phaseService,
            IMapper mapper)
        {
            _timeEntryService = timeEntryService;
            _phaseService = phaseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var entries = await _timeEntryService.GetTimeEntriesByUserAsync(userId, fromDate, toDate);

            return Ok(entries);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTimeEntryDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _timeEntryService.CreateTimeEntryAsync(model);
            return Ok(model);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var entry = await _timeEntryService.GetTimeEntryByIdAsync(id);
            if (entry == null) return NotFound();
            return Ok(entry);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateTimeEntryDto model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _timeEntryService.UpdateTimeEntryAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _timeEntryService.DeleteTimeEntryAsync(id);
            return Ok();
        }

        [HttpPost("{id:int}/approve")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            await _timeEntryService.UpdateTimeEntryStatusAsync(id, TimeEntryStatus.Approved);
            return Ok();
        }

        [HttpPost("{id:int}/reject")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Reject(int id)
        {
            await _timeEntryService.UpdateTimeEntryStatusAsync(id, TimeEntryStatus.Submitted);
            return Ok();
        }
    }
}