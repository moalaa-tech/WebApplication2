using AutoMapper;
using CRM.WebApp.DTOs.DocumentManagement;
using CRM.WebApp.Services.DocumentManagement;
using CRM.WebApp.ViewModels.DocumentManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CRM.WebApp.Controllers.DocumentManagement
{
    public class DocumentsController : Controller
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
        public async Task<IActionResult> Index(string searchTerm = null)
        {
            IEnumerable<DocumentDto> documents;

            if (!string.IsNullOrEmpty(searchTerm))
                documents = await _documentService.SearchAsync(searchTerm);
            else
                documents = await _documentService.GetAllAsync();

            var viewModels = _mapper.Map<IEnumerable<DocumentViewModel>>(documents);
            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await documentCategoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDocumentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var categories = await documentCategoryService.GetAllCategoriesAsync();
                ViewBag.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                }).ToList();
                return View(viewModel);
            }

            try
            {
                var createDto = _mapper.Map<CreateDocumentDto>(viewModel);
                var uploadedBy = User.Identity.Name; // Get current user
                await _documentService.CreateAsync(createDto, uploadedBy);

                TempData["Success"] = "Document uploaded successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var categories = await documentCategoryService.GetAllCategoriesAsync();
                ViewBag.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                }).ToList();
                ModelState.AddModelError("", $"Error uploading document: {ex.Message}");
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categories = await documentCategoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();

            var document = await _documentService.GetByIdAsync(id);
            if (document == null)
                return NotFound();

            var viewModel = _mapper.Map<EditDocumentViewModel>(document);
            viewModel.CurrentFileName = document.FileName;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditDocumentViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                var categories = await documentCategoryService.GetAllCategoriesAsync();
                ViewBag.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                }).ToList();

                return View(viewModel);
            }

            try
            {
                var updateDto = _mapper.Map<UpdateDocumentDto>(viewModel);
                var modifiedBy = User.Identity.Name;
                await _documentService.UpdateAsync(id, updateDto, modifiedBy);

                TempData["Success"] = "Document updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                var categories = await documentCategoryService.GetAllCategoriesAsync();
                ViewBag.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                }).ToList();

                ModelState.AddModelError("", $"Error updating document: {ex.Message}");
                return View(viewModel);
            }
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var document = await _documentService.GetByIdAsync(id);
            if (document == null)
                return NotFound();

            var viewModel = _mapper.Map<DocumentViewModel>(document);
            return View(viewModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                await _documentService.DeleteAsync(id);
                TempData["Success"] = "Document deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error deleting document: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var document = await _documentService.GetByIdAsync(id);
            if (document == null)
                return NotFound();

            var viewModel = _mapper.Map<DocumentViewModel>(document);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Share(ShareDocumentRequestDto shareDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sharedByUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                    var result = await _documentService.ShareDocumentAsync(shareDto, Convert.ToInt32(sharedByUserId));

                    if (result)
                    {
                        TempData["SuccessMessage"] = "Document shared successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error sharing document";
                    }
                }

                return RedirectToAction(nameof(Details), new { id = shareDto.DocumentId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error sharing document";
                return RedirectToAction(nameof(Details), new { id = shareDto.DocumentId });
            }
        }
    }
}
