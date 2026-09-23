using CRM.WebApp.DTOs.HumanResources.JobTitle;
using CRM.WebApp.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobTitleController : ControllerBase
    {
        private readonly IJobTitleService _service;

        public JobTitleController(IJobTitleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobTitleDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.CreateAsync(model);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromBody] UpdateJobTitleDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
