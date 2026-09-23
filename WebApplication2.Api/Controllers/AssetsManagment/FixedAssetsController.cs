using CRM.Domain.Enums.AssetsManagment;
using CRM.WebApp.DTOs.AssetsManagment;
using CRM.WebApp.Services.AssetsManagment;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.AssetsManagment
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixedAssetsController : ControllerBase
    {
        private readonly IFixedAssetService _assetService;
        private readonly ILogger<FixedAssetsController> _logger;

        public FixedAssetsController(
            IFixedAssetService assetService,
            ILogger<FixedAssetsController> logger)
        {
            _assetService = assetService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var assets = await _assetService.GetAllAssetsAsync();
                return Ok(assets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving fixed assets");
                return Ok(new List<FixedAssetDto>());
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var asset = await _assetService.GetAssetByIdAsync(id);
                if (asset == null)
                {
                    return NotFound();
                }
                return Ok(asset);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving asset details");
                return BadRequest(new { message = $"Error retrieving asset details: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFixedAssetDto createDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var asset = await _assetService.CreateAssetAsync(createDto);
                    return Ok(asset);
                }

                return BadRequest(ModelState);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating fixed asset");
                return BadRequest(new { message = $"Error creating fixed asset: {ex.Message}" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateFixedAssetDto updateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var asset = await _assetService.UpdateAssetAsync(updateDto);
                    return Ok(asset);
                }

                return BadRequest(ModelState);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating fixed asset");
                return BadRequest(new { message = $"Error updating fixed asset: {ex.Message}" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _assetService.DeleteAssetAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting fixed asset");
                return BadRequest(new { message = $"Error deleting fixed asset: {ex.Message}" });
            }
        }

        [HttpGet("DepreciationSchedule/{id:int}")]
        public async Task<IActionResult> DepreciationSchedule(int id)
        {
            try
            {
                var schedule = await _assetService.GetDepreciationScheduleAsync(id);
                var asset = await _assetService.GetAssetByIdAsync(id);

                return Ok(new { assetName = asset?.Name, assetId = id, schedule });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving depreciation schedule");
                return BadRequest(new { message = $"Error retrieving depreciation schedule: {ex.Message}" });
            }
        }

        [HttpPost("CalculateDepreciation")]
        public async Task<IActionResult> CalculateDepreciation([FromQuery] int assetId, [FromQuery] DateTime asOfDate)
        {
            try
            {
                var depreciation = await _assetService.CalculateDepreciationAsync(assetId, asOfDate);
                return new JsonResult(new { success = true, depreciation });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating depreciation");
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }
    }
}