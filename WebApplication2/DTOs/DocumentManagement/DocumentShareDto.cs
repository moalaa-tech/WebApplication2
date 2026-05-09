namespace CRM.WebApp.DTOs.DocumentManagement
{
    public class DocumentShareDto
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string DocumentTitle { get; set; }
        public string SharedWithUserId { get; set; }
        public string SharedWithUserName { get; set; }
        public string SharedByUserId { get; set; }
        public string SharedByUserName { get; set; }
        public DateTime ShareDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string PermissionLevel { get; set; }
        public bool CanDownload { get; set; }
    }
}
