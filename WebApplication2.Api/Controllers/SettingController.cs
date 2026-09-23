using CRM.WebApp.DTOs.Setting;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;



        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var settings = await _settingService.GetAllSettingsAsync();
            return Ok(settings);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _settingService.GetSettingByIdAsync(id.Value);
            if (setting == null)
            {
                return NotFound();
            }
            return Ok(setting);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSettingDto createSettingDto)
        {
            if (ModelState.IsValid)
            {
                await _settingService.CreateSettingAsync(createSettingDto);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateSettingDto updateSettingDto)
        {
            if (id != updateSettingDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _settingService.UpdateSettingAsync(updateSettingDto);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (Exception) // Catch other potential exceptions during update
                {
                    // Log the exception
                    return BadRequest(new { message = "Unable to save changes. Try again, and if the problem persists, see your system administrator." });
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
                await _settingService.DeleteSettingAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}