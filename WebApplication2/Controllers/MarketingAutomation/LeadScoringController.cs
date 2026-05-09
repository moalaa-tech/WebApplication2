using AutoMapper;
using CRM.WebApp.DTOs.LeadScore;
using CRM.WebApp.Services.MarketingAutomation;
using CRM.WebApp.ViewModels.MarketingAutomation.LeadScore;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.MarketingAutomation
{
    public class LeadScoringController : Controller
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

            return View(viewModel);
        }


        [HttpGet]
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

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new LeadScoreCreateViewModel
            {
                LeadScoreCreateDto = new LeadScoreCreateDto()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeadScoreCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(viewModel.LeadScoreCreateDto);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var leadScore = await _service.GetByIdAsync(id);
            if (leadScore == null)
            {
                return NotFound();
            }

            var updateDto = _mapper.Map<LeadScoreUpdateDto>(leadScore);

            var viewModel = new LeadScoreEditViewModel
            {
                LeadScoreUpdateDto = updateDto
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeadScoreEditViewModel viewModel)
        {
            if (id != viewModel.LeadScoreUpdateDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(viewModel.LeadScoreUpdateDto);
                }
                catch (System.ArgumentException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
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

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ByType(string scoreType)
        {
            var leadScores = await _service.GetByTypeAsync(scoreType);
            return View("Index", new LeadScoreIndexViewModel
            {
                LeadScores = leadScores,
                ScoreTypeCounts = null // Not showing counts in filtered view
            });
        }
    }
}
