using CRM.Domain.Enums.SalesManagement;
using CRM.WebApp.DTOs.Lead;
using CRM.WebApp.DTOs.Opportunity;
using CRM.WebApp.Services.SalesManagement;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.SalesManagment
{
    // [Authorize(Roles = "SalesRep,Manager")]

    public class SalesController : Controller
    {
        private readonly ILeadService _leadService;
        private readonly IOpportunityService OpportunityService;
        public SalesController(ILeadService leadService, IOpportunityService _opportunityService)
        {
            _leadService = leadService;
            OpportunityService = _opportunityService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var leads = await _leadService.GetLeadsAsync();
            return View(leads);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(LeadDto lead)
        {
            if (!ModelState.IsValid) return View(lead);

            await _leadService.CreateLeadAsync(lead);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var lead = await _leadService.GetLeadAsync(id);
            return lead == null ? NotFound() : View(lead);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var lead = await _leadService.GetLeadAsync(id);
            return lead == null ? NotFound() : View(lead);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LeadDto lead)
        {
            if (!ModelState.IsValid) return View(lead);

            await _leadService.UpdateLeadAsync(lead);
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            await _leadService.DeleteLeadAsync(id);
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> ConvertToOpportunity(int id)
        {
            var lead = await _leadService.GetLeadAsync(id);
            if (lead == null) return NotFound();

            lead.Status = LeadStatus.Converted;
            await _leadService.UpdateLeadAsync(lead);

            // Create Opportunity
            var opportunity = new CreateOpportunityDto
            {
                LeadId = lead.Id,
                Title = $"Opportunity for {lead.CompanyName}",
                Stage = OpportunityStage.Prospecting,
                EstimatedValue = 0,
                CreatedAt = DateTime.UtcNow
            };

            await OpportunityService.CreateOpportunityAsync(opportunity);
            return RedirectToAction("Index", "Opportunity");
        }


        // Opportunity Management



        [HttpGet]
        public async Task<IActionResult> Opportunities()
        {
            var opportunities = await OpportunityService.GetOpportunitiesAsync();
            return View(opportunities);
        }


        [HttpGet]
        public async Task<IActionResult> Opportuniy(int? id)
        {
            var opportunities = await OpportunityService.GetOpportunityAsync(id.Value);

            return View(opportunities);
        }


        [HttpGet]
        public async Task<IActionResult> AddOpportunity(CreateOpportunityDto createOpportunity)
        {
            var opportunities = await OpportunityService.CreateOpportunityAsync(createOpportunity);
            return View(opportunities);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOpportunity(UpdateOpportunityDto createOpportunity)
        {
            var opportunities = await OpportunityService.UpdateOpportunityAsync(createOpportunity);
            return View(opportunities);
        }

        [HttpPost]
        public async Task<IActionResult> AddScore(int leadId, int criteriaId, int? customPoints)
        {
            await _leadService.AddScoreToLeadAsync(leadId, criteriaId, customPoints);
            return RedirectToAction(nameof(Details), new { id = leadId });
        }
    }

}
