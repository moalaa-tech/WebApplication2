using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Company
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string NameAr { get; set; }
        
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        
        public string Address { get; set; }
        
        public string City { get; set; }
        
        public int UserId { get; set; }
        public string UserName { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        
        public string Email { get; set; }
    }
}