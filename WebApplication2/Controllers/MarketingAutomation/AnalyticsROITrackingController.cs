using AutoMapper;
using CRM.WebApp.DTOs.MarketingAutomation.CampaignAnalytics;
using CRM.WebApp.Services.MarketingAutomation.CampaignAnalytics;
using CRM.WebApp.ViewModels.MarketingAutomation.CampaignAnalytics;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.MarketingAutomation
{
    public class AnalyticsROITrackingController : Controller
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

            return View(viewModel);
        }

        [HttpGet]
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

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CampaignAnalyticsCreateViewModel
            {
                CampaignAnalyticsCreateDto = new CampaignAnalyticsCreateDto
                {
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(30)
                }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CampaignAnalyticsCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _analyticsService.AddAnalyticsAsync(viewModel.CampaignAnalyticsCreateDto);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var analytics = await _analyticsService.GetAnalyticsByIdAsync(id);
            if (analytics == null)
            {
                return NotFound();
            }

            var viewModel = new CampaignAnalyticsEditViewModel
            {
                CampaignAnalyticsUpdateDto = _mapper.Map<CampaignAnalyticsUpdateDto>(analytics)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CampaignAnalyticsEditViewModel viewModel)
        {
            if (id != viewModel.CampaignAnalyticsUpdateDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _analyticsService.UpdateAnalyticsAsync(viewModel.CampaignAnalyticsUpdateDto);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
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

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _analyticsService.DeleteAnalyticsAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult ROICalculator()
        {
            return View(new ROICalculatorViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> ROICalculator(ROICalculatorViewModel viewModel)
        {
            if (viewModel.Investment > 0)
            {
                viewModel.CalculatedROI = ((viewModel.Revenue - viewModel.Investment) / viewModel.Investment) * 100;
            }

            viewModel.ChannelROIs = await _analyticsService.GetChannelPerformanceMetricsAsync();

            return View(viewModel);
        }

    }
}
