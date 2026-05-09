namespace CRM.WebApp.DTOs.DocumentManagement
{
    public class FolderTreeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? ParentFolderId { get; set; }
        public int DocumentCount { get; set; }
        public List<FolderTreeDto> Children { get; set; }
    }
}
