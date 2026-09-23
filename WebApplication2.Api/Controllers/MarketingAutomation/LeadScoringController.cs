using AutoMapper;
using CRM.WebApp.DTOs.LeadScore;
using CRM.WebApp.Services.MarketingAutomation;
using CRM.WebApp.ViewModels.MarketingAutomation.LeadScore;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.MarketingAutomation
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadScoringController : ControllerBase
    {
        private readonly ILeadScoreService _service;
        private readonly IMapper _mapper;

        public LeadScoringController(ILeadScoreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var leadScores = await _service.GetAllAsync();
            var scoreTypes = new[] { "Behavioral", "Demographic", "Engagement", "Financial", "Social" };

            var viewModel = new LeadScoreIndexViewModel
            {
                LeadScores = leadScores,
                ScoreTypeCounts = scoreTypes.ToDictionary(
                    st => st,
                    st => leadScores.Count(ls => ls.ScoreType == st))
            };

            return Ok(viewModel);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var leadScore = await _service.GetByIdAsync(id);
            if (leadScore == null)
            {
                return NotFound();
            }

            var viewModel = new LeadScoreDetailsViewModel
            {
                LeadScore = leadScore
            };

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeadScoreCreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.CreateAsync(viewModel.LeadScoreCreateDto);
            return Ok(viewModel);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] LeadScoreEditViewModel viewModel)
        {
            if (id != viewModel.LeadScoreUpdateDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _service.UpdateAsync(viewModel.LeadScoreUpdateDto);
            }
            catch (System.ArgumentException)
            {
                return NotFound();
            }
            return Ok(viewModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("ByType")]
        public async Task<IActionResult> ByType([FromQuery] string? scoreType)
        {
            var leadScores = await _service.GetByTypeAsync(scoreType);
            return Ok(new LeadScoreIndexViewModel
            {
                LeadScores = leadScores,
                ScoreTypeCounts = null // Not showing counts in filtered view
            });
        }
    }
}
