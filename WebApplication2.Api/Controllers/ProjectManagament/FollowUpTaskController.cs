using CRM.WebApp.DTOs.FollowUpTask;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.ProjectManagament
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowUpTaskController : ControllerBase
    {

        private readonly IFollowUpTaskService _taskService;

        public FollowUpTaskController(IFollowUpTaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET: FollowUpTasks/Details/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // POST: FollowUpTasks/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFollowUpTaskDto taskDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _taskService.CreateTaskAsync(taskDto);
            return Ok(taskDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateFollowUpTaskDto taskDto)
        {
            if (id != taskDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _taskService.UpdateTaskAsync(taskDto);
            return Ok(taskDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return Ok();
        }


        [HttpPost("MarkComplete")]
        public async Task<IActionResult> MarkComplete([FromQuery] int id)
        {
            var task = await _taskService.MarkComplete(id);
            return Ok(task);
        }


    }
}