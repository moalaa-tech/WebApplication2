using CRM.WebApp.DTOs.HumanResources.Payroll;
using CRM.WebApp.Services.HumanResources;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.Controllers.HumanResources
{
    public class PayrollsController : Controller
    {
        private readonly IPayrollService _payrollService;
        private readonly IEmployeeService _employeeService; // Add employee service

        public PayrollsController(IPayrollService payrollService, IEmployeeService employeeService)
        {
            _payrollService = payrollService;
            _employeeService = employeeService; // Initialize employee service
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayrollDto>>> Index([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var payrolls = await _payrollService.GetPayrollsByPeriodAsync(startDate, endDate);
            return View(payrolls);
        }

        [HttpGet]
        public async Task<IActionResult> Process()
        {
            // Get employees from your employee service
            var employees = await _employeeService.GetAllEmployeesAsync();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");
            return View(new CreatePayrollDto());
        }


        [HttpPost]
        public async Task<IActionResult> Process(CreatePayrollDto dto)
        {
            if (!ModelState.IsValid)
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                ViewBag.Employees = new SelectList(employees, "Id", "Name");
                return View(dto);
            }

            await _payrollService.ProcessPayrollAsync(dto);
            return RedirectToAction(nameof(Index));
        }

      
    }
}
