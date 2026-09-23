using CRM.WebApp.Services.CustomerSupport_Service;
using CRM.WebApp.ViewModels.CustomerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.CustomerService
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SupportManager")]
    public class SupportAgentsController : ControllerBase
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var agents = await _agentService.GetAllAgentsAsync();
                return Ok(agents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving support agents");
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var agent = await _agentService.GetAgentByIdAsync(id);
                if (agent == null)
                {
                    return NotFound();
                }
                return Ok(agent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving agent details for ID: {id}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] SupportAgentCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var agentId = await _agentService.CreateAgentAsync(viewModel);
                return Ok(agentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating support agent");
                return BadRequest(new { message = "Error creating agent. Please try again." });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,SupportManager")]
        public async Task<IActionResult> Edit(int id, [FromBody] SupportAgentEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _agentService.UpdateAgentAsync(viewModel);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating agent with ID: {id}");
                return BadRequest(new { message = "Error updating agent. Please try again." });
            }
        }

        [HttpPost("ToggleStatus")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleStatus([FromQuery] int? id, [FromQuery] bool? isActive)
        {
            if (id == null || isActive == null)
            {
                return BadRequest();
            }

            try
            {
                await _agentService.ToggleAgentStatusAsync(id.Value, isActive.Value);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error toggling agent status for ID: {id}");
                return StatusCode(500);
            }
        }
    }
}