namespace CRM.WebApp.DTOs.DocumentManagement
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileExtension { get; set; }
        public string MimeType { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? FolderId { get; set; }
        public string FolderName { get; set; }
        public int Version { get; set; }
        public bool IsLatestVersion { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsPublic { get; set; }
        public string Status { get; set; }
        public string Tags { get; set; }
        public int DownloadCount { get; set; }
        public string FileSizeFormatted { get; set; }
    }
}
