using CRM.WebApp.DTOs.DocumentManagement;

namespace CRM.WebApp.Services.DocumentManagement
{
    public interface IDocumentFolderService
    {
        Task<IEnumerable<DocumentFolderDto>> GetAllFoldersAsync();
        Task<DocumentFolderDto> GetFolderByIdAsync(int id);
        Task<DocumentFolderDto> CreateFolderAsync(CreateDocumentFolderDto createDto);
        Task<DocumentFolderDto> UpdateFolderAsync(UpdateDocumentFolderDto updateDto);
        Task<bool> DeleteFolderAsync(int id);
        Task<bool> MoveFolderAsync(int folderId, int? newParentFolderId);
        Task<IEnumerable<FolderTreeDto>> GetFolderTreeAsync();
        Task<string> GenerateFolderPathAsync(int folderId);
        Task<bool> CanDeleteFolderAsync(int folderId);
        Task<int> GetFolderDocumentCountAsync(int folderId);
        Task<int> GetFolderSubFolderCountAsync(int folderId);
    }
}
