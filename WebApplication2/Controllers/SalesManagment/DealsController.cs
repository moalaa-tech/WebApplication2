using AutoMapper;
using CRM.WebApp.DTOs.Deal;
using CRM.WebApp.Services.SalesManagement;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.SalesManagment
{
    public class DealsController : Controller
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
                return View(deals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting deals for index view");
                return View("Error");
            }
        }

        [HttpGet]
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
                return View(deal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting deal details for ID {id}");
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDealDto createDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dealDto = await _dealService.CreateDealAsync(createDto);
                    return RedirectToAction(nameof(Details), new { id = dealDto.Id });
                }
                return View(createDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new deal");
                ModelState.AddModelError("", "An error occurred while creating the deal. Please try again.");
                return View(createDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var deal = await _dealService.GetDealByIdAsync(id);
                if (deal == null)
                {
                    _logger.LogWarning($"Deal with ID {id} not found for edit");
                    return NotFound();
                }
                return View(_mapper.Map<UpdateDealDto>(deal));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting deal for edit with ID {id}");
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateDealDto updateDto)
        {
            try
            {
                if (id != updateDto.Id)
                {
                    _logger.LogWarning($"ID mismatch in edit request for deal {id}");
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    await _dealService.UpdateDealAsync(id, updateDto);
                    return RedirectToAction(nameof(Details), new { id });
                }
                return View(updateDto);
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
                return View(updateDto);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deal = await _dealService.GetDealByIdAsync(id);
                if (deal == null)
                {
                    _logger.LogWarning($"Deal with ID {id} not found for delete confirmation");
                    return NotFound();
                }
                return View(deal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting deal for delete confirmation with ID {id}");
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _dealService.DeleteDealAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting deal {id}");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadDealFile(int dealId)
        {
            var deal = await _dealService.GetDealByIdAsync(dealId);
            return View(deal);
        }


        [HttpPost]
        public async Task<IActionResult> UploadDealFile(int dealId, IFormFile file)
        {
            try
            {
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
