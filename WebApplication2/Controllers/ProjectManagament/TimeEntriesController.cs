using AutoMapper;
using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Project;
using CRM.WebApp.Services.ProjectManagment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRM.WebApp.Controllers.Project
{
    [Authorize]
    [Route("time-entries")]
    public class TimeEntriesController : Controller
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
        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var entries = await _timeEntryService.GetTimeEntriesByUserAsync(userId, fromDate, toDate);

            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            return View(entries);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create(int? phaseId)
        {
            var model = new CreateTimeEntryDto
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                EntryDate = DateTime.Today,
                IsBillable = true
            };

            if (phaseId.HasValue)
            {
                var phase = await _phaseService.GetPhaseByIdAsync(phaseId.Value);
                if (phase != null)
                {
                    model.JobPhaseId = phaseId.Value;
                    ViewBag.PhaseName = phase.Name;
                }
            }

            return View(model);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTimeEntryDto model)
        {
            if (ModelState.IsValid)
            {
                await _timeEntryService.CreateTimeEntryAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var entry = await _timeEntryService.GetTimeEntryByIdAsync(id);
            if (entry == null) return NotFound();
            return View(entry);
        }

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var entry = await _timeEntryService.GetTimeEntryByIdAsync(id);
            if (entry == null) return NotFound();

            var model = _mapper.Map<UpdateTimeEntryDto>(entry);
            return View(model);
        }

        [HttpPost("{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateTimeEntryDto model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _timeEntryService.UpdateTimeEntryAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var entry = await _timeEntryService.GetTimeEntryByIdAsync(id);
            if (entry == null) return NotFound();
            return View(entry);
        }

        [HttpPost("{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _timeEntryService.DeleteTimeEntryAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}/approve")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            await _timeEntryService.UpdateTimeEntryStatusAsync(id, TimeEntryStatus.Approved);
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost("{id}/reject")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Reject(int id)
        {
            await _timeEntryService.UpdateTimeEntryStatusAsync(id, TimeEntryStatus.Submitted);
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
