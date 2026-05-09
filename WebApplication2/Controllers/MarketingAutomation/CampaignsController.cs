using AutoMapper;
using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.DTOs.MarketingAutomation.Campaign;
using CRM.WebApp.Services.MarketingAutomation;
using CRM.WebApp.ViewModels.MarketingAutomation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.Controllers.MarketingAutomation
{
    public class CampaignsController : Controller
    {
        private readonly ICampaignService _campaignService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IMapper _mapper;

        public CampaignsController(
            ICampaignService campaignService,
            IEmailTemplateService emailTemplateService,
            IMapper mapper)
        {
            _campaignService = campaignService;
            _emailTemplateService = emailTemplateService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var campaigns = await _campaignService.GetAllCampaignsAsync();
            return View(campaigns);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var campaign = await _campaignService.GetCampaignByIdAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CampaignDetailsViewModel>(campaign);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var emailTemplates = await _emailTemplateService.GetAllAsync();
            var viewModel = new CampaignCreateViewModel
            {
                EmailTemplateOptions = new SelectList(emailTemplates, "Id", "Name")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CampaignCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var campaignDto = _mapper.Map<CreateCampaignDto>(viewModel);
                await _campaignService.CreateCampaignAsync(campaignDto);
                return RedirectToAction(nameof(Index));
            }

            // Reload email templates if validation fails
            viewModel.EmailTemplateOptions = new SelectList(await _emailTemplateService.GetAllAsync(), "Id", "Name");
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var campaign = await _campaignService.GetCampaignByIdAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            var emailTemplates = await _emailTemplateService.GetAllAsync();
            var viewModel = _mapper.Map<CampaignEditViewModel>(campaign);
            viewModel.EmailTemplateOptions = new SelectList(emailTemplates, "Id", "Name", campaign.EmailTemplateId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CampaignEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var campaignDto = _mapper.Map<UpdateCampaignDto>(viewModel);
                    await _campaignService.UpdateCampaignAsync(id, campaignDto);
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            viewModel.EmailTemplateOptions = new SelectList(
                await _emailTemplateService.GetAllAsync(), "Id", "Name", viewModel.EmailTemplateId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, CampaignStatus status)
        {
            try
            {
                await _campaignService.ChangeCampaignStatusAsync(id, status);
                TempData["SuccessMessage"] = "Campaign status updated successfully";
            }
            catch (Exception)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
