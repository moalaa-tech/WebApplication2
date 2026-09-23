using AutoMapper;
using CRM.WebApp.DTOs.Deal;
using CRM.WebApp.Services.SalesManagement;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.SalesManagment
{
    [ApiController]
    [Route("api/[controller]")]
    public class DealsController : ControllerBase
    {
        private readonly IDealService _dealService;
        private readonly ILogger<DealsController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        public DealsController(
            IDealService dealService,
            ILogger<DealsController> logger,
            IMapper mapper,
            IWebHostEnvironment hostingEnvironment)
        {
            _dealService = dealService;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var deals = await _dealService.GetAllDealsAsync();
                return Ok(deals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting deals for index view");
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var deal = await _dealService.GetDealByIdAsync(id);
                if (deal == null)
                {
                    _logger.LogWarning($"Deal with ID {id} not found");
                    return NotFound();
                }
                return Ok(deal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting deal details for ID {id}");
                return StatusCode(500);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDealDto createDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var dealDto = await _dealService.CreateDealAsync(createDto);
                return Ok(dealDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new deal");
                ModelState.AddModelError("", "An error occurred while creating the deal. Please try again.");
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateDealDto updateDto)
        {
            try
            {
                if (id != updateDto.Id)
                {
                    _logger.LogWarning($"ID mismatch in edit request for deal {id}");
                    return NotFound();
                }

                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _dealService.UpdateDealAsync(id, updateDto);
                return Ok(updateDto);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Deal with ID {id} not found for update");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating deal {id}");
                ModelState.AddModelError("", "An error occurred while updating the deal. Please try again.");
                return BadRequest(ModelState);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _dealService.DeleteDealAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting deal {id}");
                return StatusCode(500);
            }
        }

        [HttpPost("UploadDealFile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadDealFile()
        {
            try
            {
                var file = Request.Form.Files["file"];
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file selected");
                }

                var webRootPath = _hostingEnvironment.WebRootPath;
                var fileName = await _dealService.SaveFileAsync(file, webRootPath);

                // Here you would typically save the file reference to the database
                // using the repository's AddFileAsync method

                return Ok(new { fileName });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while uploading file");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}