using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Customer;
using CRM.WebApp.DTOs.OrderDetails;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Order
{
    public class UpdateOrderDto
    {
        [Required]
        public int Id { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }


        public string? Note { get; set; }

        public int? CustomerId { get; set; }

        [Display(Name = "Product")]
        public int? ProductId { get; set; }


        public InvoiceStatus Status { get; set; }

        [Required(ErrorMessage = "Order Number is required.")]
        [StringLength(100, ErrorMessage = "Order Number cannot exceed 100 characters.")]
        public string? OrderNumber { get; set; }

        [Required(ErrorMessage = "Order Date is required.")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Total Amount must be greater than zero.")]
        public decimal TotalAmount { get; set; }


        public IEnumerable<OrderDetailsDto>? OrderDetails { get; set; }

        public CustomerDto? Customer { get; set; }


    }
}
