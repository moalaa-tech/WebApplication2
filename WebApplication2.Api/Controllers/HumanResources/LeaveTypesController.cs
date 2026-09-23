using CRM.WebApp.DTOs.HumanResources.LeaveType;
using CRM.WebApp.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveTypesController : ControllerBase
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypesController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var leaveTypes = await _leaveTypeService.GetAllLeaveTypesAsync();
            return Ok(leaveTypes);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var leaveType = await _leaveTypeService.GetLeaveTypeByIdAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            return Ok(leaveType);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLeaveTypeDto createLeaveTypeDto)
        {
            if (ModelState.IsValid)
            {
                await _leaveTypeService.CreateLeaveTypeAsync(createLeaveTypeDto);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateLeaveTypeDto updateLeaveTypeDto)
        {
            if (id != updateLeaveTypeDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _leaveTypeService.UpdateLeaveTypeAsync(updateLeaveTypeDto);
                }
                catch (Exception)
                {
                    if (await _leaveTypeService.GetLeaveTypeByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _leaveTypeService.DeleteLeaveTypeAsync(id);
            return Ok();
        }
    }
}
