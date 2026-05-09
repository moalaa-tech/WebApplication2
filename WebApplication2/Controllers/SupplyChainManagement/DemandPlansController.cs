using AutoMapper;
using CRM.Domain.Enums.SupplyChainManagement;
using CRM.WebApp.DTOs.SupplyChainManagement;
using CRM.WebApp.Services.SupplyChainManagement;
using CRM.WebApp.ViewModels.SupplyChainManagement.DemandPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.Controllers.SupplyChainManagement
{
    //[Authorize]
    //[Route("scm/demand-plans")]
    public class DemandPlansController : Controller
    {
        private readonly IDemandPlanService _demandPlanService;
        private readonly IMapper _mapper;
        private readonly ILogger<DemandPlansController> _logger;


        public DemandPlansController(IDemandPlanService demandPlanService, IMapper mapper, ILogger<DemandPlansController> logger)
        {
            _demandPlanService = demandPlanService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var demandPlans = await _demandPlanService.GetAllDemandPlansAsync();
            return View(demandPlans);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var demandPlan = await _demandPlanService.GetDemandPlanByIdAsync(id);
            if (demandPlan == null)
            {
                return NotFound();
            }

            return View(demandPlan);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DemandPlanCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DemandPlanCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var createDto = _mapper.Map<DemandPlanCreateDto>(model);
                await _demandPlanService.CreateDemandPlanAsync(createDto);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var demandPlan = await _demandPlanService.GetDemandPlanByIdAsync(id);
            if (demandPlan == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<DemandPlanUpdateViewModel>(demandPlan);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DemandPlanUpdateViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updateDto = _mapper.Map<DemandPlanUpdateDto>(model);
                    await _demandPlanService.UpdateDemandPlanAsync(id, updateDto);
                }
                catch
                {
                    if (!await DemandPlanExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _demandPlanService.DeleteDemandPlanAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DemandPlanExists(int id)
        {
            var demandPlan = await _demandPlanService.GetDemandPlanByIdAsync(id);
            return demandPlan != null;
        }

        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var dashboard = await _demandPlanService.GetDashboardDataAsync();
                return View(dashboard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving dashboard data");
                TempData["ErrorMessage"] = "Error retrieving dashboard data";
                return View(new SupplyChainDashboardDto());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int planId, DemandPlanItemCreateDto itemDto)
        {
            try
            {
                var result = await _demandPlanService.AddItemToPlanAsync(planId, itemDto);
                if (result)
                {
                    TempData["SuccessMessage"] = "Item added successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error adding item";
                }
                return RedirectToAction(nameof(Details), new { id = planId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding item to plan");
                TempData["ErrorMessage"] = "Error adding item to plan";
                return RedirectToAction(nameof(Details), new { id = planId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItemStatus(int itemId, bool isProcured)
        {
            try
            {
                var result = await _demandPlanService.UpdateItemProcurementStatusAsync(itemId, isProcured);
                return Json(new { success = result, message = result ? "Status updated successfully" : "Error updating status" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating item status");
                return Json(new { success = false, message = "Error updating status" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateForecast(int planId)
        {
            try
            {
                var result = await _demandPlanService.GenerateForecastAsync(planId);
                if (result)
                {
                    TempData["SuccessMessage"] = "Forecast generated successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error generating forecast";
                }
                return RedirectToAction(nameof(Details), new { id = planId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating forecast");
                TempData["ErrorMessage"] = "Error generating forecast";
                return RedirectToAction(nameof(Details), new { id = planId });
            }
        }
        private void PopulateStatusDropdown()
        {
            var statuses = Enum.GetValues(typeof(DemandPlanStatus))
                .Cast<DemandPlanStatus>()
                .Select(s => new SelectListItem
                {
                    Value = ((int)s).ToString(),
                    Text = s.ToString()
                })
                .ToList();

            ViewBag.Statuses = statuses;
        }

        private void PopulateCostTypeDropdown()
        {
            var costTypes = Enum.GetValues(typeof(CostType))
                .Cast<CostType>()
                .Select(ct => new SelectListItem
                {
                    Value = ((int)ct).ToString(),
                    Text = ct.ToString()
                })
                .ToList();

            ViewBag.CostTypes = costTypes;
        }

    }

    // Similar controllers for other SCM modules...
}
