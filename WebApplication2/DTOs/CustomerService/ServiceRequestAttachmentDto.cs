namespace CRM.WebApp.DTOs.CustomerService
{
    public class ServiceRequestAttachmentDto
    {
        public int Id { get; set; }
        public int ServiceRequestId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadDate { get; set; }

        // Additional metadata
        public string UploadedBy { get; set; } // Name of the uploader
        public string FileSizeFormatted { get; set; } // Human-readable size (e.g., "2.5 MB")
    }
}
