namespace CRM.WebApp.DTOs.EmailTemplate
{
    public class EmailTemplateListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public bool IsActive { get; set; }
    }
}
