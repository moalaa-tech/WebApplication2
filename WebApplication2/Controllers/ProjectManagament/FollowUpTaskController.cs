using CRM.WebApp.DTOs.FollowUpTask;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.ProjectManagament
{
    public class FollowUpTaskController : Controller
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
            return View(tasks);
        }

        // GET: FollowUpTasks/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: FollowUpTasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFollowUpTaskDto taskDto)
        {
            if (ModelState.IsValid)
            {
                await _taskService.CreateTaskAsync(taskDto);
                return RedirectToAction(nameof(Index));
            }
            return View(taskDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateFollowUpTaskDto taskDto)
        {
            if (id != taskDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _taskService.UpdateTaskAsync(taskDto);
                return RedirectToAction(nameof(Index));
            }
            return View(taskDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> MarkComplete(int id)
        {
            var task = await _taskService.MarkComplete(id);
            return RedirectToAction(nameof(Details), new { id = task.Id });
        }



    }
}
