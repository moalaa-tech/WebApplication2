using AutoMapper;
using CRM.WebApp.DTOs.SupplyChainManagement.ForecastData;
using CRM.WebApp.Services.SupplyChainManagement;
using CRM.WebApp.ViewModels.SupplyChainManagement.ForecastData;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.SupplyChainManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForecastDataController : ControllerBase
    {
        private readonly IForecastDataService _forecastDataService;
        private readonly IMapper _mapper;

        public ForecastDataController(IForecastDataService forecastDataService, IMapper mapper)
        {
            _forecastDataService = forecastDataService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var forecastData = await _forecastDataService.GetAllForecastDataAsync();
            var viewModels = _mapper.Map<IEnumerable<ForecastDataViewModel>>(forecastData);
            return Ok(viewModels);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var forecastData = await _forecastDataService.GetForecastDataByIdAsync(id);
            if (forecastData == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ForecastDataViewModel>(forecastData);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateForecastDataViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var createDto = _mapper.Map<CreateForecastDataDto>(viewModel);
                await _forecastDataService.CreateForecastDataAsync(createDto);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error creating forecast data: {ex.Message}" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EditForecastDataViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updateDto = _mapper.Map<UpdateForecastDataDto>(viewModel);
                await _forecastDataService.UpdateForecastDataAsync(updateDto);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error updating forecast data: {ex.Message}" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _forecastDataService.DeleteForecastDataAsync(id);
            return Ok();
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> Filter()
        {
            var viewModel = new ForecastDataFilterViewModel();
            return Ok(viewModel);
        }

        [HttpPost("Filter")]
        public async Task<IActionResult> Filter([FromBody] ForecastDataFilterViewModel viewModel)
        {
            var results = await _forecastDataService.GetFilteredForecastDataAsync(
                viewModel.DataType,
                viewModel.StartDate,
                viewModel.EndDate,
                viewModel.DemandPlanId);

            viewModel.Results = _mapper.Map<List<ForecastDataViewModel>>(results);
            return Ok(viewModel);
        }

        [HttpGet("Summary")]
        public async Task<IActionResult> Summary()
        {
            var summary = new
            {
                Total = await _forecastDataService.GetForecastSummaryAsync("total", null, null),
                Average = await _forecastDataService.GetForecastSummaryAsync("average", null, null),
                Max = await _forecastDataService.GetForecastSummaryAsync("max", null, null),
                Min = await _forecastDataService.GetForecastSummaryAsync("min", null, null)
            };

            return Ok(summary);
        }
    }
}