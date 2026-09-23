using AutoMapper;
using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.DTOs.MarketingAutomation.Campaign;
using CRM.WebApp.Services.MarketingAutomation;
using CRM.WebApp.ViewModels.MarketingAutomation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Api.Controllers.MarketingAutomation
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : ControllerBase
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
            return Ok(campaigns);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var campaign = await _campaignService.GetCampaignByIdAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CampaignDetailsViewModel>(campaign);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CampaignCreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var campaignDto = _mapper.Map<CreateCampaignDto>(viewModel);
            await _campaignService.CreateCampaignAsync(campaignDto);
            return Ok(viewModel);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CampaignEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var campaignDto = _mapper.Map<UpdateCampaignDto>(viewModel);
                await _campaignService.UpdateCampaignAsync(id, campaignDto);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(viewModel);
        }

        [HttpPost("ChangeStatus/{id:int}")]
        public async Task<IActionResult> ChangeStatus(int id, [FromQuery] CampaignStatus status)
        {
            try
            {
                await _campaignService.ChangeCampaignStatusAsync(id, status);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
