using CRM.WebApp.DTOs.HumanResources.JobTitle;
using CRM.WebApp.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.HumanResources
{
    public class JobTitleController : Controller
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
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _service.GetCreateModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobTitleDto model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = await _service.GetCreateModelAsync().ContinueWith(t => t.Result.Departments);
                return View(model);
            }

            await _service.CreateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateJobTitleDto model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = await _service.GetCreateModelAsync().ContinueWith(t => t.Result.Departments);
                return View(model);
            }

            await _service.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
