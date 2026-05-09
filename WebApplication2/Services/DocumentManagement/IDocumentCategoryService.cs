using CRM.WebApp.DTOs.DocumentManagement;

namespace CRM.WebApp.Services.DocumentManagement
{
    public interface IDocumentCategoryService
    {
        Task<IEnumerable<DocumentCategoryDto>> GetAllCategoriesAsync();
        Task<DocumentCategoryDto> GetCategoryByIdAsync(int id);
        Task<DocumentCategoryDto> CreateCategoryAsync(CreateDocumentCategoryDto createDto);
        Task<DocumentCategoryDto> UpdateCategoryAsync(int id, UpdateDocumentCategoryDto updateDto);
        Task<bool> DeleteCategoryAsync(int id);
        Task<IEnumerable<DocumentCategoryDto>> GetActiveCategoriesAsync();
        Task<IEnumerable<DocumentCategoryDto>> GetCategoriesWithDocumentsAsync();
    }
}
