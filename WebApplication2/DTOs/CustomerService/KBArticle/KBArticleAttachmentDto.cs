namespace CRM.WebApp.DTOs.CustomerService.KBArticle
{
    public class KBArticleAttachmentDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileSize { get; set; } // Human-readable (e.g., "2.5 MB")
        public string DownloadUrl { get; set; }
        public string PreviewUrl { get; set; } // For image/video previews
        public DateTime UploadDate { get; set; }
        public string Description { get; set; }
    }
}
