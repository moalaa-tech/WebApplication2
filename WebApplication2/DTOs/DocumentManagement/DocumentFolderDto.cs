using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.DocumentManagement
{
    public class DocumentFolderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentFolderId { get; set; }
        public string ParentFolderName { get; set; }
        public string Path { get; set; }
        public bool IsSystemFolder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int DocumentCount { get; set; }
        public int SubFolderCount { get; set; }
        public List<DocumentFolderDto> SubFolders { get; set; }
    }
}
