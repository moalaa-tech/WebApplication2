using AutoMapper;
using CRM.WebApp.DTOs.CustomerService.ServiceRequest;
using CRM.WebApp.Services.CustomerSupport_Service;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.CustomerService;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.CustomerService
{
    public class ServiceRequestController : Controller
    {
        private readonly IServiceRequestService _service;
        // declare auto mapper for
        private readonly IMapper mapper;


        public ServiceRequestController(IServiceRequestService service,
            IMapper _mapper
            )
        {
            _service = service;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var requests = await _service.GetAllServiceRequestsAsync();
            return View(requests);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServiceRequestViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var dto = mapper.Map<ServiceRequestCreateDto>(vm);
            await _service.CreateServiceRequestAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var request = await _service.GetServiceRequestByIdAsync(id);
            if (request == null) return NotFound();
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceRequestViewModel vm)
        {
            if (id != vm.Id) return NotFound();
            if (!ModelState.IsValid) return View(vm);
            var dto = mapper.Map<ServiceRequestUpdateDto>(vm);

            await _service.UpdateServiceRequestAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _service.GetServiceRequestByIdAsync(id);
            if (request == null) return NotFound();
            return View(request);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteServiceRequestAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var request = await _service.GetServiceRequestByIdAsync(id);
            if (request == null) return NotFound();
            return View(request);
        }
    }
}
