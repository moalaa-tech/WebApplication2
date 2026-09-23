using CRM.Domain.Enums.SalesManagement;
using CRM.WebApp.DTOs.Lead;
using CRM.WebApp.DTOs.Opportunity;
using CRM.WebApp.Services.SalesManagement;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.SalesManagment
{
    // [Authorize(Roles = "SalesRep,Manager")]
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
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
            return Ok(leads);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeadDto lead)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _leadService.CreateLeadAsync(lead);
            return Ok(lead);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var lead = await _leadService.GetLeadAsync(id);
            return lead == null ? NotFound() : Ok(lead);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] LeadDto lead)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _leadService.UpdateLeadAsync(lead);
            return Ok(lead);
        }

        //[Authorize(Roles = "Manager")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _leadService.DeleteLeadAsync(id);
            return Ok();
        }



        [HttpPost("ConvertToOpportunity")]
        public async Task<IActionResult> ConvertToOpportunity([FromQuery] int id)
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
            return Ok();
        }


        // Opportunity Management



        [HttpGet("Opportunities")]
        public async Task<IActionResult> Opportunities()
        {
            var opportunities = await OpportunityService.GetOpportunitiesAsync();
            return Ok(opportunities);
        }


        [HttpGet("Opportuniy")]
        public async Task<IActionResult> Opportuniy([FromQuery] int? id)
        {
            var opportunities = await OpportunityService.GetOpportunityAsync(id.Value);

            return Ok(opportunities);
        }


        [HttpGet("AddOpportunity")]
        public async Task<IActionResult> AddOpportunity([FromQuery] CreateOpportunityDto createOpportunity)
        {
            var opportunities = await OpportunityService.CreateOpportunityAsync(createOpportunity);
            return Ok(opportunities);
        }

        [HttpGet("UpdateOpportunity")]
        public async Task<IActionResult> UpdateOpportunity([FromQuery] UpdateOpportunityDto createOpportunity)
        {
            var opportunities = await OpportunityService.UpdateOpportunityAsync(createOpportunity);
            return Ok(opportunities);
        }

        [HttpPost("AddScore")]
        public async Task<IActionResult> AddScore([FromQuery] int leadId, [FromQuery] int criteriaId, [FromQuery] int? customPoints)
        {
            await _leadService.AddScoreToLeadAsync(leadId, criteriaId, customPoints);
            return Ok();
        }
    }

}