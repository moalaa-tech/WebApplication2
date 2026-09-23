using AutoMapper;
using CRM.WebApp.DTOs.MarketingAutomation.CampaignAnalytics;
using CRM.WebApp.Services.MarketingAutomation.CampaignAnalytics;
using CRM.WebApp.ViewModels.MarketingAutomation.CampaignAnalytics;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.MarketingAutomation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsROITrackingController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly IMapper _mapper;

        public AnalyticsROITrackingController(IAnalyticsService analyticsService, IMapper mapper)
        {
            _analyticsService = analyticsService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var analytics = await _analyticsService.GetAllAnalyticsAsync();
            var channelPerformance = await _analyticsService.GetChannelPerformanceMetricsAsync();
            var totalROI = await _analyticsService.CalculateTotalROIAsync();

            var viewModel = new CampaignAnalyticsIndexViewModel
            {
                Analytics = analytics,
                ChannelPerformance = channelPerformance,
                TotalROI = totalROI
            };

            return Ok(viewModel);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var analytics = await _analyticsService.GetAnalyticsByIdAsync(id);
            if (analytics == null)
            {
                return NotFound();
            }

            var viewModel = new CampaignAnalyticsDetailsViewModel
            {
                CampaignAnalytics = analytics
            };

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CampaignAnalyticsCreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _analyticsService.AddAnalyticsAsync(viewModel.CampaignAnalyticsCreateDto);
            return Ok(viewModel);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CampaignAnalyticsEditViewModel viewModel)
        {
            if (id != viewModel.CampaignAnalyticsUpdateDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _analyticsService.UpdateAnalyticsAsync(viewModel.CampaignAnalyticsUpdateDto);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return Ok(viewModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _analyticsService.DeleteAnalyticsAsync(id);
            return Ok();
        }


        [HttpGet("ROICalculator")]
        public IActionResult ROICalculator()
        {
            return Ok(new ROICalculatorViewModel());
        }


        [HttpPost("ROICalculator")]
        public async Task<IActionResult> ROICalculator([FromBody] ROICalculatorViewModel viewModel)
        {
            if (viewModel.Investment > 0)
            {
                viewModel.CalculatedROI = ((viewModel.Revenue - viewModel.Investment) / viewModel.Investment) * 100;
            }

            viewModel.ChannelROIs = await _analyticsService.GetChannelPerformanceMetricsAsync();

            return Ok(viewModel);
        }

    }
}
