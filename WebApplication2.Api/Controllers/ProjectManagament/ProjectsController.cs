using AutoMapper;
using CRM.Domain.Enums.ProjectManagment;
using CRM.WebApp.DTOs.Project;
using CRM.WebApp.Services.CustomerSupport_Service;
using CRM.WebApp.Services.ProjectManagment;
using CRM.WebApp.ViewModels.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Api.Controllers.ProjectManagament
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, ICustomerService customerService, IMapper mapper)
        {
            _projectService = projectService;
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            var viewModels = _mapper.Map<IEnumerable<ProjectDetailsViewModel>>(projects);
            return Ok(viewModels);

        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            var costSummary = await _projectService.GetProjectCostSummaryAsync(id);
            var timeEntries = await _projectService.GetTimeEntriesForProjectAsync(id, null, null);
            //var expenses = await _projectService.GetExpensesForProjectAsync(id, null, null);
            var viewModel = _mapper.Map<ProjectDetailsViewModel>(project);
            viewModel.CostSummary =_mapper.Map<ProjectCostSummaryViewModel>(costSummary);
            viewModel.RecentTimeEntries =_mapper.Map<List<TimeEntryViewModel>>(timeEntries);
            //var viewModel = new ProjectDetailsViewModel
            //{
            //    // Project = project,
            //    //CostSummary = costSummary,
            //    //TimeEntries = timeEntries,
            //    //Expenses = expenses
            //};

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectCreateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var project = _mapper.Map<CreateProjectDto>(model);
            await _projectService.CreateProjectAsync(project);
            return Ok(model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateProjectDto model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _projectService.UpdateProjectAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return Ok();
        }

        [HttpGet("{id:int}/cost-summary")]
        public async Task<IActionResult> CostSummary(int id)
        {
            var summary = await _projectService.GetProjectCostSummaryAsync(id);
            return Ok(summary);
        }

        [HttpGet("{id:int}/time-entries")]
        public async Task<IActionResult> TimeEntries(int id, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var entries = await _projectService.GetTimeEntriesForProjectAsync(id, fromDate, toDate);
            return Ok(entries);
        }


        [HttpGet("{id:int}/expenses")]
        public async Task<IActionResult> Expenses(int id, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var expenses = await _projectService.GetExpensesForProjectAsync(id, fromDate, toDate);
            return Ok(expenses);
        }
    }
}