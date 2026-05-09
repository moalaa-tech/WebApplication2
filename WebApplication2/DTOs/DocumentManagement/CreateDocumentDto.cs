namespace CRM.WebApp.DTOs.DocumentManagement
{
    public class CreateDocumentDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DocumentType { get; set; }
        public int? CategoryId { get; set; }
        public int? FolderId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsPublic { get; set; }
        public string Tags { get; set; }
        public IFormFile File { get; set; }
    }
}
