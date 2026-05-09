namespace CRM.WebApp.DTOs.EmailTemplate
{
    public class UpdateEmailTemplateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
