using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Customer;
using CRM.WebApp.DTOs.OrderDetails;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int? CustomerId { get; set; }
        public CustomerDto? Customer { get; set; }
        public string? OrderNumber { get; set; }

        public InvoiceStatus Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }


        public decimal TotalAmount { get; set; }
        public int? StatesId { get; set; }
        public IEnumerable<OrderDetailsDto>? OrderDetails { get; set; }
    }
}
