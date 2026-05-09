using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.InventoryManagement.ProductType
{
    public class UpdateProductTypeDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}
