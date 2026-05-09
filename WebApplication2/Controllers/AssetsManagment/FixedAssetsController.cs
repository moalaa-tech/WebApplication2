using CRM.Domain.Enums.AssetsManagment;
using CRM.WebApp.DTOs.AssetsManagment;
using CRM.WebApp.Services.AssetsManagment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.Controllers.AssetsManagment
{
    public class FixedAssetsController : Controller
    {
        private readonly IFixedAssetService _assetService;
        private readonly ILogger<FixedAssetsController> _logger;

        public FixedAssetsController(
            IFixedAssetService assetService,
            ILogger<FixedAssetsController> logger)
        {
            _assetService = assetService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var assets = await _assetService.GetAllAssetsAsync();
                return View(assets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving fixed assets");
                TempData["ErrorMessage"] = "Error retrieving fixed assets";
                return View(new List<FixedAssetDto>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var asset = await _assetService.GetAssetByIdAsync(id);
                if (asset == null)
                {
                    return NotFound();
                }
                return View(asset);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving asset details");
                TempData["ErrorMessage"] = "Error retrieving asset details";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateParentAssetsDropdown();
            await PopulateDepreciationMethodsDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFixedAssetDto createDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var asset = await _assetService.CreateAssetAsync(createDto);
                    TempData["SuccessMessage"] = "Fixed asset created successfully";
                    return RedirectToAction(nameof(Details), new { id = asset.Id });
                }

                await PopulateParentAssetsDropdown();
                await PopulateDepreciationMethodsDropdown();
                return View(createDto);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                await PopulateParentAssetsDropdown();
                await PopulateDepreciationMethodsDropdown();
                return View(createDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating fixed asset");
                TempData["ErrorMessage"] = "Error creating fixed asset";
                await PopulateParentAssetsDropdown();
                await PopulateDepreciationMethodsDropdown();
                return View(createDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var asset = await _assetService.GetAssetByIdAsync(id);
                if (asset == null)
                {
                    return NotFound();
                }

                var updateDto = new UpdateFixedAssetDto
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    Description = asset.Description,
                    AcquisitionDate = asset.AcquisitionDate,
                    AcquisitionCost = asset.AcquisitionCost,
                    SalvageValue = asset.SalvageValue,
                    UsefulLife = asset.UsefulLife,
                    DepreciationMethod = asset.DepreciationMethod,
                    ParentAssetId = asset.ParentAssetId,
                    IsActive = asset.IsActive
                };

                await PopulateParentAssetsDropdown(asset.Id);
                await PopulateDepreciationMethodsDropdown();
                return View(updateDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving asset for edit");
                TempData["ErrorMessage"] = "Error retrieving asset";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateFixedAssetDto updateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var asset = await _assetService.UpdateAssetAsync(updateDto);
                    TempData["SuccessMessage"] = "Fixed asset updated successfully";
                    return RedirectToAction(nameof(Details), new { id = asset.Id });
                }

                await PopulateParentAssetsDropdown(updateDto.Id);
                await PopulateDepreciationMethodsDropdown();
                return View(updateDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating fixed asset");
                TempData["ErrorMessage"] = "Error updating fixed asset";
                await PopulateParentAssetsDropdown(updateDto.Id);
                await PopulateDepreciationMethodsDropdown();
                return View(updateDto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _assetService.DeleteAssetAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Fixed asset deleted successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Fixed asset not found";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting fixed asset");
                TempData["ErrorMessage"] = "Error deleting fixed asset";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DepreciationSchedule(int id)
        {
            try
            {
                var schedule = await _assetService.GetDepreciationScheduleAsync(id);
                var asset = await _assetService.GetAssetByIdAsync(id);

                ViewBag.AssetName = asset?.Name;
                ViewBag.AssetId = id;

                return View(schedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving depreciation schedule");
                TempData["ErrorMessage"] = "Error retrieving depreciation schedule";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CalculateDepreciation(int assetId, DateTime asOfDate)
        {
            try
            {
                var depreciation = await _assetService.CalculateDepreciationAsync(assetId, asOfDate);
                return Json(new { success = true, depreciation });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating depreciation");
                return Json(new { success = false, message = ex.Message });
            }
        }

        private async Task PopulateParentAssetsDropdown(int? excludeAssetId = null)
        {
            var assets = await _assetService.GetAllAssetsAsync();
            var selectList = assets
                .Where(a => !excludeAssetId.HasValue || a.Id != excludeAssetId.Value)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = $"{a.AssetNumber} - {a.Name}"
                })
                .ToList();

            selectList.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewBag.ParentAssets = selectList;
        }

        private async Task PopulateDepreciationMethodsDropdown()
        {
            var methods = Enum.GetValues(typeof(DepreciationMethod))
                .Cast<DepreciationMethod>()
                .Select(m => new SelectListItem
                {
                    Value = ((int)m).ToString(),
                    Text = m.ToString()
                })
                .ToList();

            ViewBag.DepreciationMethods = methods;
        }

    }
}
