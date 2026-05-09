using CRM.WebApp.Services.CustomerSupport_Service;
using CRM.WebApp.ViewModels.CustomerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.CustomerService
{
    [Authorize(Roles = "Admin,SupportManager")]
    public class SupportAgentsController : Controller
    {
        private readonly ISupportAgentService _agentService;
        private readonly ILogger<SupportAgentsController> _logger;

        public SupportAgentsController(
            ISupportAgentService agentService,
            ILogger<SupportAgentsController> logger)
        {
            _agentService = agentService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var agents = await _agentService.GetAllAgentsAsync();
                return View(agents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving support agents");
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var agent = await _agentService.GetAgentByIdAsync(id);
                if (agent == null)
                {
                    return NotFound();
                }
                return View(agent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving agent details for ID: {id}");
                return View("Error");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(SupportAgentCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var agentId = await _agentService.CreateAgentAsync(viewModel);
                return RedirectToAction(nameof(Details), new { id = agentId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating support agent");
                ModelState.AddModelError("", "Error creating agent. Please try again.");
                return View(viewModel);
            }
        }

        [Authorize(Roles = "Admin,SupportManager")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var agent = await _agentService.GetAgentByIdAsync(id);
                if (agent == null)
                {
                    return NotFound();
                }

                var editViewModel = new SupportAgentEditViewModel
                {
                    Id = agent.Id,
                    FirstName = agent.FullName.Split(' ')[0],
                    LastName = agent.FullName.Split(' ')[1],
                    Email = agent.Email,
                    PhoneNumber = agent.PhoneNumber,
                    JobTitle = agent.JobTitle,
                    Department = agent.Department,
                    Skills = string.Join(", ", agent.Skills),
                    Specialization = agent.Specialization,
                    WorkingHours = agent.WorkingHours,
                    IsActive = agent.IsActive
                };

                return View(editViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving agent for edit with ID: {id}");
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SupportManager")]
        public async Task<IActionResult> Edit(int id, SupportAgentEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                await _agentService.UpdateAgentAsync(viewModel);
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating agent with ID: {id}");
                ModelState.AddModelError("", "Error updating agent. Please try again.");
                return View(viewModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleStatus(int id, bool isActive)
        {
            try
            {
                await _agentService.ToggleAgentStatusAsync(id, isActive);
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error toggling agent status for ID: {id}");
                return View("Error");
            }
        }
    }
}
