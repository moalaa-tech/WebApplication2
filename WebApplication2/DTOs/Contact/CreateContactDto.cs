namespace CRM.WebApp.DTOs.Contact
{
    public class CreateContactDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int CompanyId { get; set; }
    }

}
