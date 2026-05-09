using AutoMapper;
using CRM.WebApp.DTOs.CustomerService;
using CRM.WebApp.Services.CustomerSupport_Service;
using CRM.WebApp.ViewModels.CustomerService.ServiceLevelAgreement;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.CustomerService
{
    public class ServiceLevelAgreementsController : Controller
    {
        private readonly ISLAService _slaService;
        private readonly IMapper _mapper;

        public ServiceLevelAgreementsController(
            ISLAService slaService,
            IMapper mapper)
        {
            _slaService = slaService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var slas = await _slaService.GetAllActiveSLAsAsync();
            var viewModels = _mapper.Map<IEnumerable<ServiceLevelAgreementViewModel>>(slas);
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServiceLevelAgreementViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                var createDto = _mapper.Map<SLACreateDto>(viewModel);
                await _slaService.CreateSLAAsync(createDto);

                TempData["Success"] = "SLA created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating SLA: {ex.Message}");
                return View(viewModel);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sla = await _slaService.GetSLADetailsAsync(id);
            if (sla == null)
                return NotFound();

            var viewModel = _mapper.Map<EditServiceLevelAgreementViewModel>(sla);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditServiceLevelAgreementViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                var updateDto = _mapper.Map<SLAUpdateDto>(viewModel);
                await _slaService.UpdateSLAAsync(updateDto);

                TempData["Success"] = "SLA updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating SLA: {ex.Message}");
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var sla = await _slaService.GetSLADetailsAsync(id);
            if (sla == null)
                return NotFound();

            var viewModel = _mapper.Map<ServiceLevelAgreementViewModel>(sla);
            return View(viewModel);
        }

        
    }
}
