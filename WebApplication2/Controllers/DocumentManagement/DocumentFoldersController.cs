using CRM.WebApp.DTOs.DocumentManagement;
using CRM.WebApp.Services.DocumentManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.Controllers.DocumentManagement
{
    public class DocumentFoldersController : Controller
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
                return View(folders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving folders");
                TempData["ErrorMessage"] = "Error retrieving folders";
                return View(new List<DocumentFolderDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var folder = await _folderService.GetFolderByIdAsync(id);
                if (folder == null)
                {
                    return NotFound();
                }
                return View(folder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving folder details");
                TempData["ErrorMessage"] = "Error retrieving folder details";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateParentFoldersDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDocumentFolderDto createDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var folder = await _folderService.CreateFolderAsync(createDto);
                    TempData["SuccessMessage"] = "Folder created successfully";
                    return RedirectToAction(nameof(Details), new { id = folder.Id });
                }

                await PopulateParentFoldersDropdown();
                return View(createDto);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                await PopulateParentFoldersDropdown();
                return View(createDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating folder");
                TempData["ErrorMessage"] = "Error creating folder";
                await PopulateParentFoldersDropdown();
                return View(createDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var folder = await _folderService.GetFolderByIdAsync(id);
                if (folder == null)
                {
                    return NotFound();
                }

                var updateDto = new UpdateDocumentFolderDto
                {
                    Id = folder.Id,
                    Name = folder.Name,
                    Description = folder.Description,
                    ParentFolderId = folder.ParentFolderId,
                    IsSystemFolder = folder.IsSystemFolder,
                    IsActive = folder.IsActive
                };

                await PopulateParentFoldersDropdown(folder.Id);
                return View(updateDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving folder for edit");
                TempData["ErrorMessage"] = "Error retrieving folder";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateDocumentFolderDto updateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var folder = await _folderService.UpdateFolderAsync(updateDto);
                    TempData["SuccessMessage"] = "Folder updated successfully";
                    return RedirectToAction(nameof(Details), new { id = folder.Id });
                }

                await PopulateParentFoldersDropdown(updateDto.Id);
                return View(updateDto);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                await PopulateParentFoldersDropdown(updateDto.Id);
                return View(updateDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating folder");
                TempData["ErrorMessage"] = "Error updating folder";
                await PopulateParentFoldersDropdown(updateDto.Id);
                return View(updateDto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _folderService.DeleteFolderAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Folder deleted successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Folder not found";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting folder");
                TempData["ErrorMessage"] = "Error deleting folder";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> TreeView()
        {
            try
            {
                var folderTree = await _folderService.GetFolderTreeAsync();
                return View(folderTree);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving folder tree");
                TempData["ErrorMessage"] = "Error retrieving folder structure";
                return View(new List<FolderTreeDto>());
            }
        }

        private async Task PopulateParentFoldersDropdown(int? excludeFolderId = null)
        {
            var folders = await _folderService.GetAllFoldersAsync();
            var selectList = folders
                .Where(f => !excludeFolderId.HasValue || f.Id != excludeFolderId.Value)
                .Select(f => new SelectListItem
                {
                    Value = f.Id.ToString(),
                    Text = f.Path,
                    Disabled = f.IsSystemFolder
                })
                .ToList();

            selectList.Insert(0, new SelectListItem { Value = "", Text = "Root Folder" });
            ViewBag.ParentFolders = selectList;
        }



       
    }
}
