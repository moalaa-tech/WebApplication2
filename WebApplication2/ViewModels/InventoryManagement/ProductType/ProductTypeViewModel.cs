using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.InventoryManagement.ProductType
{
    public class ProductTypeViewModel
    {
        public int Id { get; set; }

        [DisplayName("Product Type Name")]
        public string Name { get; set; }

        [DisplayName("Arabic Product Type Name")]
        public string NameAr { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
    }
}