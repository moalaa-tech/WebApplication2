using CRM.WebApp.DTOs.HumanResources.Department;
using CRM.WebApp.Hubs;
using CRM.WebApp.Services.HumanResources;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;

namespace CRM.WebApp.Controllers.HumanResources
{
    [AllowAnonymous]
    public class DepartmentController : Controller
    {
        public readonly IDepartmentService _departmentService;
        public readonly IEmployeeService employeeService;

        //public readonly IHubContext<DepartmentHub> _hubContext;
            
        public DepartmentController(
            IDepartmentService departmentService, 
            IEmployeeService employeeService
           // IHubContext<DepartmentHub> hubContext
           )
        {
            _departmentService = departmentService;
            this.employeeService = employeeService;
           // _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var controller = HttpContext.Request.RouteValues["controller"]?.ToString();
            var action = HttpContext.Request.RouteValues["action"]?.ToString();


            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateManagersDropDownList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentDto createDepartmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _departmentService.CreateDepartmentAsync(createDepartmentDto);
                    // Notify clients
                    //await _hubContext.Clients.All.SendAsync("DepartmentAdded", createDepartmentDto);


                    return RedirectToAction(nameof(Index));
                }
                catch (KeyNotFoundException ex)
                {
                    ModelState.AddModelError("ManagerId", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to create department. Please try again.");
                    // Log the exception (ex)
                }
            }
            await PopulateManagersDropDownList(createDepartmentDto.ManagerId);
            return View(createDepartmentDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            // Map DepartmentDto back to UpdateDepartmentDto for the view
            var updateDepartmentDto = new UpdateDepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                Location = department.Location,
                Budget = department.Budget,
                EstablishedDate = department.EstablishedDate,
                ManagerId = department.ManagerName == "N/A" ? null : department.ManagerId // Handle manager ID for dropdown
            };
            await PopulateManagersDropDownList(updateDepartmentDto.ManagerId);
            return View(updateDepartmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Location,Budget,EstablishedDate,ManagerId")] UpdateDepartmentDto updateDepartmentDto)
        {
            if (id != updateDepartmentDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _departmentService.UpdateDepartmentAsync(updateDepartmentDto);
                }
                catch (KeyNotFoundException ex)
                {
                    ModelState.AddModelError("ManagerId", ex.Message);
                    // No need to redirect, just show validation error on the page
                }
                catch (Exception)
                {
                    // Log the exception
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                if (ModelState.IsValid) // Check again if no new errors were added
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            await PopulateManagersDropDownList(updateDepartmentDto.ManagerId);
            return View(updateDepartmentDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _departmentService.DeleteDepartmentAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // Helper to populate the Managers dropdown
        private async Task PopulateManagersDropDownList(object selectedManager = null)
        {
            var employees = await employeeService.GetAllEmployeesAsync();
            // It's good practice to allow an "unassigned" option for nullable foreign keys
            ViewBag.ManagerId = new SelectList(employees, "Id", "Name", selectedManager);
        }
    }
}
