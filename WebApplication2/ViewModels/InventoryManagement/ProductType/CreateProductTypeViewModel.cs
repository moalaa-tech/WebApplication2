using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.InventoryManagement.ProductType
{
    public class CreateProductTypeViewModel
    {
        [Required(ErrorMessage = "Product Type Name is required.")]
        [StringLength(200, ErrorMessage = "Product Type Name cannot exceed 200 characters.")]
        [DisplayName("Product Type Name")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Arabic Product Type Name cannot exceed 200 characters.")]
        [DisplayName("Arabic Product Type Name")]
        public string NameAr { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}