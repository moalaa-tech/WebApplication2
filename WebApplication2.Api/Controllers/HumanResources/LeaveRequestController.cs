using CRM.WebApp.DTOs.HumanResources.LeaveRequest;
using CRM.WebApp.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _service;

        public LeaveRequestController(ILeaveRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var requests = await _service.GetAllLeaveRequestsAsync();
            return Ok(requests);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLeaveRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.CreateLeaveRequestAsync(dto);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var leaveRequest = await _service.GetLeaveRequestByIdAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            return Ok(leaveRequest);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateLeaveRequestDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateLeaveRequestAsync(id, updateDto);
                }
                catch (Exception)
                {
                    if (await _service.GetLeaveRequestByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteLeaveRequestAsync(id);
            return Ok();
        }
    }
}
