using CRM.Domain.Base;


namespace CRM.Domain.Entities.CustomerService
{
    public class ServiceRequestAttachment : BaseEntity
    {
        public int ServiceRequestId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadDate { get; set; }

        public ServiceRequest ServiceRequest { get; set; }
    }
}
