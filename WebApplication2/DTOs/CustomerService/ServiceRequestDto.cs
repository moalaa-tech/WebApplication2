using System;

namespace CRM.WebApp.DTOs.CustomerService
{
    public class ServiceRequestDto
    {
        public int Id { get; set; }
        public string RequestType { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ResolutionNotes { get; set; }
    }
}