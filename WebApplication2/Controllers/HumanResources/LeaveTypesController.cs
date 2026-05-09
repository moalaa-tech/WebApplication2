using CRM.WebApp.DTOs.HumanResources.LeaveType;
using CRM.WebApp.Services.HumanResources;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.HumanResources
{
    public class LeaveTypesController : Controller
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
            return View(leaveTypes);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var leaveType = await _leaveTypeService.GetLeaveTypeByIdAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            return View(leaveType);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLeaveTypeDto createLeaveTypeDto)
        {
            if (ModelState.IsValid)
            {
                await _leaveTypeService.CreateLeaveTypeAsync(createLeaveTypeDto);
                return RedirectToAction(nameof(Index));
            }
            return View(createLeaveTypeDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var leaveType = await _leaveTypeService.GetLeaveTypeByIdAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateLeaveTypeDto
            {
                Id = leaveType.Id,
                Name = leaveType.Name,
                NameAr = leaveType.NameAr
            };

            return View(updateDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateLeaveTypeDto updateLeaveTypeDto)
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
                return RedirectToAction(nameof(Index));
            }
            return View(updateLeaveTypeDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var leaveType = await _leaveTypeService.GetLeaveTypeByIdAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _leaveTypeService.DeleteLeaveTypeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
