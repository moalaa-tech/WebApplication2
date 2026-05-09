using AutoMapper;
using CRM.WebApp.DTOs.SupplyChainManagement.ForecastData;
using CRM.WebApp.Services.SupplyChainManagement;
using CRM.WebApp.ViewModels.SupplyChainManagement.ForecastData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.SupplyChainManagement
{
    public class ForecastDataController : Controller
    {
        private readonly IForecastDataService _forecastDataService;
        private readonly IMapper _mapper;

        public ForecastDataController(IForecastDataService forecastDataService, IMapper mapper)
        {
            _forecastDataService = forecastDataService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var forecastData = await _forecastDataService.GetAllForecastDataAsync();
            var viewModels = _mapper.Map<IEnumerable<ForecastDataViewModel>>(forecastData);
            return View(viewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var forecastData = await _forecastDataService.GetForecastDataByIdAsync(id);
            if (forecastData == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ForecastDataViewModel>(forecastData);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateForecastDataViewModel
            {
                DataDate = DateTime.Today
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateForecastDataViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createDto = _mapper.Map<CreateForecastDataDto>(viewModel);
                    await _forecastDataService.CreateForecastDataAsync(createDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating forecast data: {ex.Message}");
                }
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var forecastData = await _forecastDataService.GetForecastDataByIdAsync(id);
            if (forecastData == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<EditForecastDataViewModel>(forecastData);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditForecastDataViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updateDto = _mapper.Map<UpdateForecastDataDto>(viewModel);
                    await _forecastDataService.UpdateForecastDataAsync(updateDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating forecast data: {ex.Message}");
                }
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var forecastData = await _forecastDataService.GetForecastDataByIdAsync(id);
            if (forecastData == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ForecastDataViewModel>(forecastData);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _forecastDataService.DeleteForecastDataAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Filter()
        {
            var viewModel = new ForecastDataFilterViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Filter(ForecastDataFilterViewModel viewModel)
        {
            var results = await _forecastDataService.GetFilteredForecastDataAsync(
                viewModel.DataType,
                viewModel.StartDate,
                viewModel.EndDate,
                viewModel.DemandPlanId);

            viewModel.Results = _mapper.Map<List<ForecastDataViewModel>>(results);
            return View(viewModel);
        }

        public async Task<IActionResult> Summary()
        {
            var summary = new
            {
                Total = await _forecastDataService.GetForecastSummaryAsync("total", null, null),
                Average = await _forecastDataService.GetForecastSummaryAsync("average", null, null),
                Max = await _forecastDataService.GetForecastSummaryAsync("max", null, null),
                Min = await _forecastDataService.GetForecastSummaryAsync("min", null, null)
            };

            return View(summary);
        }
   }
}

