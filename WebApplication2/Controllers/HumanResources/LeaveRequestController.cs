using CRM.WebApp.DTOs.HumanResources.LeaveRequest;
using CRM.WebApp.Services.HumanResources;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.HumanResources
{
    public class LeaveRequestController : Controller
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
            return View(requests);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveRequestDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _service.CreateLeaveRequestAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var leaveRequest = await _service.GetLeaveRequestByIdAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            return View(leaveRequest);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var leaveRequest = await _service.GetLeaveRequestByIdAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            return View(leaveRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateLeaveRequestDto updateDto)
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
                return RedirectToAction(nameof(Index));
            }
            return View(updateDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var leaveRequest = await _service.GetLeaveRequestByIdAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            return View(leaveRequest);
        }

        // POST: LeaveRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteLeaveRequestAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
