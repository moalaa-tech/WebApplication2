using AutoMapper;
using CRM.WebApp.DTOs.Project;
using CRM.WebApp.Services.ProjectManagment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.ProjectManagament
{
    //[Authorize]
    //[Route("projects/{projectId}/phases")]
    [ApiController]
    [Route("api/[controller]")]
    public class JobPhasesController : ControllerBase
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
        public async Task<IActionResult> Index([FromQuery] int projectId)
        {
            var project = await _projectService.GetProjectByIdAsync(projectId);
            if (project == null) return NotFound();

            var phases = await _phaseService.GetPhasesByProjectAsync(projectId);
            return Ok(phases);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] int projectId, [FromBody] CreateJobPhaseDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _phaseService.CreatePhaseAsync(model);
            return Ok(model);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details([FromQuery] int projectId, int id)
        {
            var phase = await _phaseService.GetPhaseByIdAsync(id);
            if (phase == null || phase.ProjectId != projectId) return NotFound();

            return Ok(phase);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromQuery] int projectId, int id, [FromBody] UpdateJobPhaseDto model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _phaseService.UpdatePhaseAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfirmed([FromQuery] int projectId, int id)
        {
            await _phaseService.DeletePhaseAsync(id);
            return Ok();
        }

        [HttpGet("{id:int}/cost-summary")]
        public async Task<IActionResult> CostSummary([FromQuery] int projectId, int id)
        {
            var summary = await _phaseService.GetPhaseCostSummaryAsync(id);
            return Ok(summary);
        }

        [HttpGet("{id:int}/time-entries")]
        public async Task<IActionResult> TimeEntries([FromQuery] int projectId, int id, DateTime? fromDate, DateTime? toDate)
        {
            var entries = await _phaseService.GetTimeEntriesForPhaseAsync(id, fromDate, toDate);
            return Ok(entries);
        }

        [HttpGet("{id:int}/expenses")]
        public async Task<IActionResult> Expenses([FromQuery] int projectId, int id, DateTime? fromDate, DateTime? toDate)
        {
            var expenses = await _phaseService.GetExpensesForPhaseAsync(id, fromDate, toDate);
            return Ok(expenses);
        }
    }
}