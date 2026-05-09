using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Business
{
    public class BusinessViewModel
    {
        public int Id { get; set; }

        [DisplayName("Business Name")]
        public string Name { get; set; }

        [DisplayName("Arabic Business Name")]
        public string NameAr { get; set; }
    }
}