using AutoMapper;
using CRM.WebApp.DTOs.MarketingAutomation.Segment;
using CRM.WebApp.Services.MarketingAutomation.Segment;
using CRM.WebApp.ViewModels.MarketingAutomation.Segment;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.MarketingAutomation
{
    [ApiController]
    [Route("api/[controller]")]
    public class SegmentationController : ControllerBase
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
            return Ok(viewModel);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var segment = await _segmentService.GetSegmentByIdAsync(id);
            if (segment == null)
            {
                return NotFound();
            }

            var viewModel = new SegmentIndexViewModel();
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SegmentCreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //await _segmentService.AddSegmentAsync(viewModel);
            return Ok(viewModel);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] SegmentEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                //await _segmentService.UpdateSegmentAsync(viewModel);
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
            await _segmentService.DeleteSegmentAsync(id);
            return Ok();
        }

    }
}
