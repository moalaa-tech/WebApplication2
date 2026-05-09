using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.InventoryManagement.ProductType
{
    public class CreateProductTypeDto
    {
        [Required]
        public required string Name { get; set; }
        public required string NameAR { get; set; }


        public string? Description { get; set; }

    }
}
