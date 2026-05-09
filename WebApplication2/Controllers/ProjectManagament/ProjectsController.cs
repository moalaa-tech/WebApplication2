using AutoMapper;
using CRM.Domain.Enums.ProjectManagment;
using CRM.WebApp.DTOs.Project;
using CRM.WebApp.Services.CustomerSupport_Service;
using CRM.WebApp.Services.ProjectManagment;
using CRM.WebApp.ViewModels.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.Controllers.Project
{
    public class ProjectsController : Controller
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
            return View(viewModels);

        }


        [HttpGet]
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

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            var viewModel = new ProjectCreateViewModel
            {
                Customers = customers.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = _mapper.Map<CreateProjectDto>(model);
                await _projectService.CreateProjectAsync(project);
                return RedirectToAction(nameof(Index));
            }

            // Repopulate customers if model is invalid
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            model.Customers = (await _customerService.GetAllCustomersAsync())
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            var model = _mapper.Map<UpdateProjectDto>(project);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateProjectDto model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _projectService.UpdateProjectAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CostSummary(int id)
        {
            var summary = await _projectService.GetProjectCostSummaryAsync(id);
            return View(summary);
        }

        [HttpGet]
        public async Task<IActionResult> TimeEntries(int id, DateTime? fromDate, DateTime? toDate)
        {
            var entries = await _projectService.GetTimeEntriesForProjectAsync(id, fromDate, toDate);
            ViewBag.ProjectId = id;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            return View(entries);
        }


        [HttpGet]
        public async Task<IActionResult> Expenses(int id, DateTime? fromDate, DateTime? toDate)
        {
            var expenses = await _projectService.GetExpensesForProjectAsync(id, fromDate, toDate);
            ViewBag.ProjectId = id;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            return View(expenses);
        }
    }
}
