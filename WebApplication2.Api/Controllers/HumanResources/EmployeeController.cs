using AutoMapper;
using CRM.WebApp.DTOs.HumanResources.Employee;
using CRM.WebApp.Services.HumanResources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class EmployeeController : ControllerBase
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
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
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
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.CreateEmployeeAsync(employeeDto);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateEmployeeDto employeeDto)
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
                catch (Exception)
                {
                    // Log the exception
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("OrganizationChart")]
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

            return Ok(chartData);
        }
    }
}
