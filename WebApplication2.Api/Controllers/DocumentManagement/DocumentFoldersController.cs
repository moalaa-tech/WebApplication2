using CRM.WebApp.DTOs.DocumentManagement;
using CRM.WebApp.Services.DocumentManagement;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.DocumentManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentFoldersController : ControllerBase
    {
        private readonly IDocumentFolderService _folderService;
        private readonly ILogger<DocumentFoldersController> _logger;

        public DocumentFoldersController(
            IDocumentFolderService folderService,
            ILogger<DocumentFoldersController> logger)
        {
            _folderService = folderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var folders = await _folderService.GetAllFoldersAsync();
                return Ok(folders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving folders");
                return StatusCode(500, new { message = "Error retrieving folders" });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var folder = await _folderService.GetFolderByIdAsync(id);
                if (folder == null)
                {
                    return NotFound();
                }
                return Ok(folder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving folder details");
                return StatusCode(500, new { message = "Error retrieving folder details" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDocumentFolderDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var folder = await _folderService.CreateFolderAsync(createDto);
                return Ok(folder);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating folder");
                return StatusCode(500, new { message = "Error creating folder" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateDocumentFolderDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var folder = await _folderService.UpdateFolderAsync(updateDto);
                return Ok(folder);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating folder");
                return StatusCode(500, new { message = "Error updating folder" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _folderService.DeleteFolderAsync(id);
                if (result)
                {
                    return Ok();
                }

                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting folder");
                return StatusCode(500, new { message = "Error deleting folder" });
            }
        }

        [HttpGet("TreeView")]
        public async Task<IActionResult> TreeView()
        {
            try
            {
                var folderTree = await _folderService.GetFolderTreeAsync();
                return Ok(folderTree);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving folder tree");
                return StatusCode(500, new { message = "Error retrieving folder structure" });
            }
        }
    }
}