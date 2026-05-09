using CRM.WebApp.DTOs.DocumentManagement;

namespace CRM.WebApp.Services.DocumentManagement
{
    public interface IDocumentService
    {
        Task<DocumentDto> GetByIdAsync(int id);
        Task<IEnumerable<DocumentDto>> GetAllAsync();
        Task<IEnumerable<DocumentDto>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<DocumentDto>> GetByFolderAsync(int folderId);
        Task<IEnumerable<DocumentDto>> SearchAsync(string searchTerm);
        Task<DocumentDto> CreateAsync(CreateDocumentDto createDto, string uploadedBy);
        Task UpdateAsync(int id, UpdateDocumentDto updateDto, string modifiedBy);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task IncrementDownloadCountAsync(int id);
        Task<Stream> GetFileStreamAsync(int id);


        Task<bool> ShareDocumentAsync(ShareDocumentRequestDto shareDto, int sharedByUserId);
        Task<IEnumerable<DocumentShareDto>> GetDocumentSharesAsync(int documentId);
        Task<bool> RemoveShareAsync(int shareId);
        Task<string> GenerateDownloadUrlAsync(int documentId);
        Task<bool> IncrementViewCountAsync(int documentId);

    }
}
