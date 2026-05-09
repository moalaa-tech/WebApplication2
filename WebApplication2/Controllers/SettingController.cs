using CRM.WebApp.DTOs.Setting;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApp.Controllers
{
    public class SettingController : Controller
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
            return View(settings);
        }

        [HttpGet]
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
            return View(setting);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSettingDto createSettingDto)
        {
            if (ModelState.IsValid)
            {
                await _settingService.CreateSettingAsync(createSettingDto);
                return RedirectToAction(nameof(Index));
            }
            return View(createSettingDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
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

            // Map SettingDto back to UpdateSettingDto for the view
            var updateSettingDto = new UpdateSettingDto
            {
                Id = setting.Id,
                Name = setting.Name,
                NameAr = setting.NameAr
            };
            return View(updateSettingDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] UpdateSettingDto updateSettingDto)
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
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(updateSettingDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
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
            return View(setting);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _settingService.DeleteSettingAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
