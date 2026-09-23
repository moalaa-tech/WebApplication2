using CRM.WebApp.DTOs.Customer;
using CRM.WebApp.Services.CustomerSupport_Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.SalesManagment
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int page = 1)
        {
            var pageSize = 10;

            var customers = await _customerService.GetAllCustomersAsync(page, pageSize);
            return Ok(customers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto createCustomerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _customerService.CreateCustomerAsync(createCustomerDto);
            return Ok(createCustomerDto);
        }

        // POST: EmployeeController/Edit/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateCustomerDto updateCustomerDto)
        {
            if (id != updateCustomerDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _customerService.UpdateCustomerAsync(updateCustomerDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return Ok(updateCustomerDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}