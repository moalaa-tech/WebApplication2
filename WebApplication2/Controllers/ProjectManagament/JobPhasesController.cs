using AutoMapper;
using CRM.WebApp.DTOs.Project;
using CRM.WebApp.Services.ProjectManagment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.Project
{
    //[Authorize]
    //[Route("projects/{projectId}/phases")]
    public class JobPhasesController : Controller
    {
        private readonly IJobPhaseService _phaseService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public JobPhasesController(
            IJobPhaseService phaseService,
            IProjectService projectService,
            IMapper mapper)
        {
            _phaseService = phaseService;
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int projectId)
        {
            var project = await _projectService.GetProjectByIdAsync(projectId);
            if (project == null) return NotFound();

            var phases = await _phaseService.GetPhasesByProjectAsync(projectId);
            ViewBag.ProjectId = projectId;
            ViewBag.ProjectName = project.Name;
            return View(phases);
        }

        [HttpGet]
        public IActionResult Create(int projectId)
        {
            ViewBag.ProjectId = projectId;
            return View(new CreateJobPhaseDto { ProjectId = projectId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int projectId, CreateJobPhaseDto model)
        {
            if (ModelState.IsValid)
            {
                await _phaseService.CreatePhaseAsync(model);
                return RedirectToAction(nameof(Index), new { projectId });
            }

            ViewBag.ProjectId = projectId;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int projectId, int id)
        {
            var controller = HttpContext.Request.RouteValues["controller"]?.ToString();
            var action = HttpContext.Request.RouteValues["action"]?.ToString();


            var phase = await _phaseService.GetPhaseByIdAsync(id);
            if (phase == null || phase.ProjectId != projectId) return NotFound();

            ViewBag.ProjectId = projectId;
            return View(phase);
        }

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int projectId, int id)
        {
            var phase = await _phaseService.GetPhaseByIdAsync(id);
            if (phase == null || phase.ProjectId != projectId) return NotFound();

            var model = _mapper.Map<UpdateJobPhaseDto>(phase);
            ViewBag.ProjectId = projectId;
            return View(model);
        }

        [HttpPost("{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int projectId, int id, UpdateJobPhaseDto model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _phaseService.UpdatePhaseAsync(model);
                return RedirectToAction(nameof(Index), new { projectId });
            }

            ViewBag.ProjectId = projectId;
            return View(model);
        }

        [HttpGet("{id}/delete")]
        public async Task<IActionResult> Delete(int projectId, int id)
        {
            var phase = await _phaseService.GetPhaseByIdAsync(id);
            if (phase == null || phase.ProjectId != projectId) return NotFound();

            ViewBag.ProjectId = projectId;
            return View(phase);
        }

        [HttpPost("{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int projectId, int id)
        {
            await _phaseService.DeletePhaseAsync(id);
            return RedirectToAction(nameof(Index), new { projectId });
        }

        [HttpGet("{id}/cost-summary")]
        public async Task<IActionResult> CostSummary(int projectId, int id)
        {
            var summary = await _phaseService.GetPhaseCostSummaryAsync(id);
            ViewBag.ProjectId = projectId;
            return View(summary);
        }

        [HttpGet("{id}/time-entries")]
        public async Task<IActionResult> TimeEntries(int projectId, int id, DateTime? fromDate, DateTime? toDate)
        {
            var entries = await _phaseService.GetTimeEntriesForPhaseAsync(id, fromDate, toDate);
            ViewBag.ProjectId = projectId;
            ViewBag.PhaseId = id;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            return View(entries);
        }

        [HttpGet("{id}/expenses")]
        public async Task<IActionResult> Expenses(int projectId, int id, DateTime? fromDate, DateTime? toDate)
        {
            var expenses = await _phaseService.GetExpensesForPhaseAsync(id, fromDate, toDate);
            ViewBag.ProjectId = projectId;
            ViewBag.PhaseId = id;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            return View(expenses);
        }
    }
}
