using AutoMapper;
using CRM.WebApp.DTOs.DocumentManagement;
using CRM.WebApp.Services.DocumentManagement;
using CRM.WebApp.ViewModels.DocumentManagement;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.DocumentManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IDocumentCategoryService documentCategoryService;
        private readonly IMapper _mapper;

        public DocumentsController(IDocumentService documentService, IMapper mapper, IDocumentCategoryService documentCategoryService)
        {
            _documentService = documentService;
            _mapper = mapper;
            this.documentCategoryService = documentCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string? searchTerm = null)
        {
            IEnumerable<DocumentDto> documents;

            if (!string.IsNullOrEmpty(searchTerm))
                documents = await _documentService.SearchAsync(searchTerm);
            else
                documents = await _documentService.GetAllAsync();

            var viewModels = _mapper.Map<IEnumerable<DocumentViewModel>>(documents);
            return Ok(viewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDocumentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createDto = _mapper.Map<CreateDocumentDto>(viewModel);
                var uploadedBy = User.Identity.Name;
                await _documentService.CreateAsync(createDto, uploadedBy);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error uploading document: {ex.Message}" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EditDocumentViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updateDto = _mapper.Map<UpdateDocumentDto>(viewModel);
                var modifiedBy = User.Identity.Name;
                await _documentService.UpdateAsync(id, updateDto, modifiedBy);

                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error updating document: {ex.Message}" });
            }
        }

        [HttpGet("Download/{id:int}")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var document = await _documentService.GetByIdAsync(id);
                if (document == null)
                    return NotFound();

                var stream = await _documentService.GetFileStreamAsync(id);
                await _documentService.IncrementDownloadCountAsync(id);

                return File(stream, document.MimeType, document.FileName);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                await _documentService.DeleteAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error deleting document: {ex.Message}" });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var document = await _documentService.GetByIdAsync(id);
            if (document == null)
                return NotFound();

            var viewModel = _mapper.Map<DocumentViewModel>(document);
            return Ok(viewModel);
        }

        [HttpPost("Share")]
        public async Task<IActionResult> Share([FromBody] ShareDocumentRequestDto shareDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sharedByUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var result = await _documentService.ShareDocumentAsync(shareDto, Convert.ToInt32(sharedByUserId));

                if (result)
                {
                    return Ok();
                }

                return BadRequest(new { message = "Error sharing document" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Error sharing document" });
            }
        }
    }
}