using CRM.WebApp.DTOs.HumanResources.Department;
using CRM.WebApp.Services.HumanResources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class DepartmentController : ControllerBase
    {
        public readonly IDepartmentService _departmentService;
        public readonly IEmployeeService employeeService;

        public DepartmentController(
            IDepartmentService departmentService,
            IEmployeeService employeeService)
        {
            _departmentService = departmentService;
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("{id:int}")]
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
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto createDepartmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _departmentService.CreateDepartmentAsync(createDepartmentDto);
                    return Ok();
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
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateDepartmentDto updateDepartmentDto)
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
                }
                catch (Exception)
                {
                    // Log the exception
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                if (ModelState.IsValid)
                {
                    return Ok();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _departmentService.DeleteDepartmentAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
