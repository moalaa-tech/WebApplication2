using AutoMapper;
using CRM.WebApp.DTOs.HumanResources.Employee;
using CRM.WebApp.Services.HumanResources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.Controllers.HumanResources
{
    [AllowAnonymous]
    public class EmployeeController : Controller
    {
        public readonly IEmployeeService _employeeService;
        public readonly IDepartmentService DepartmentService;

        public readonly IMapper _mapper;


        public EmployeeController(IEmployeeService employeeService, IMapper mapper, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            DepartmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateEmployeeDto employeeDto)
        {

            if (ModelState.IsValid)
            {
                await _employeeService.CreateEmployeeAsync(employeeDto);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDepartmentsDropDownList(employeeDto.DepartmentId.Value);
            return View(employeeDto);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            // Map EmployeeDto back to UpdateEmployeeDto for the view
            var updateEmployeeDto = new UpdateEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Description = employee.Description,
                DepartmentId = employee.DepartmentId
            };
            await PopulateDepartmentsDropDownList(updateEmployeeDto.DepartmentId);
            return View(updateEmployeeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdateEmployeeDto employeeDto)
        {

            if (id != employeeDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.UpdateEmployeeAsync(employeeDto);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (Exception) // Catch other potential exceptions during update
                {
                    // Log the exception
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            await PopulateDepartmentsDropDownList(employeeDto.DepartmentId);
            return View(employeeDto);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }


        private async Task PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departments = await DepartmentService.GetAllDepartmentsAsync();
            ViewBag.DepartmentId = new SelectList(departments, "Id", "Name", selectedDepartment);
        }

        [HttpGet]
        public async Task<IActionResult> OrganizationChart()
        {
            var employees = await _employeeService.GetAllEmployeesWithDepartmentAsync();
            var chartData = new List<object[]>();
            chartData.Add(new[] { "Name", "Manager", "ToolTip" });

            foreach (var employee in employees)
            {
                var managerName = employee.Department?.Manager?.FirstName + " " + employee.Department?.Manager?.LastName;
                chartData.Add(new object[] { employee.FirstName + " " + employee.LastName, managerName, employee.Department?.Name });
            }

            return View(chartData);
        }
    }
}
