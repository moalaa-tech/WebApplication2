using CRM.WebApp.DTOs.HumanResources.Payroll;
using CRM.WebApp.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollsController : ControllerBase
    {
        private readonly IPayrollService _payrollService;
        private readonly IEmployeeService _employeeService;

        public PayrollsController(IPayrollService payrollService, IEmployeeService employeeService)
        {
            _payrollService = payrollService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var payrolls = await _payrollService.GetPayrollsByPeriodAsync(startDate, endDate);
            return Ok(payrolls);
        }

        [HttpPost("Process")]
        public async Task<IActionResult> Process([FromBody] CreatePayrollDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _payrollService.ProcessPayrollAsync(dto);
            return Ok();
        }
    }
}