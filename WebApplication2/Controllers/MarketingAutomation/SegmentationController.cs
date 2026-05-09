using AutoMapper;
using CRM.WebApp.DTOs.MarketingAutomation.Segment;
using CRM.WebApp.Services.MarketingAutomation.Segment;
using CRM.WebApp.ViewModels.MarketingAutomation.Segment;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.MarketingAutomation
{
    public class SegmentationController : Controller
    {
        private readonly ISegmentService _segmentService;
        private readonly IMapper _mapper;

        public SegmentationController(ISegmentService segmentService, IMapper mapper)
        {
            _segmentService = segmentService;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var segments = await _segmentService.GetAllSegmentsAsync();
            var viewModel = new List<SegmentIndexViewModel>();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var segment = await _segmentService.GetSegmentByIdAsync(id);
            if (segment == null)
            {
                return NotFound();
            }

            var viewModel = new SegmentIndexViewModel();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new SegmentCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SegmentCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //await _segmentService.AddSegmentAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var segment = await _segmentService.GetSegmentByIdAsync(id);
            if (segment == null)
            {
                return NotFound();
            }

            var viewModel = new SegmentEditViewModel
            {
                //SegmentUpdateDto = _mapper.Map<SegmentUpdateDto>(segment)
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SegmentEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //await _segmentService.UpdateSegmentAsync(viewModel);
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
            var segment = await _segmentService.GetSegmentByIdAsync(id);
            if (segment == null)
            {
                return NotFound();
            }

            var viewModel = new SegmentIndexViewModel();

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _segmentService.DeleteSegmentAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
