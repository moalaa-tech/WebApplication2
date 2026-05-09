using CRM.Domain.Enums;

namespace CRM.WebApp.ViewModels.Contact
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime? LastContactDate { get; set; }
        public ContactStatus Status { get; set; }
    }

}
