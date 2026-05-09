using CRM.Domain.Enums;

namespace CRM.WebApp.DTOs.Contact
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int CompanyId { get; set; }
        public DateTime? LastContactDate { get; set; }
        public ContactStatus Status { get; set; }
    }

}
