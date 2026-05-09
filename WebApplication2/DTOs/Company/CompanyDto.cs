using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Company
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int BusinessId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int UserId { get; set; } // Display userId (could be user name if joined)
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }


        public string Email { get; set; }

    }
}
