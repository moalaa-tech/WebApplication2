namespace CRM.WebApp.DTOs.CustomerService.ServiceRequest
{
    public class SupportAgentLookupDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; } // Optional: for additional filtering
        public bool IsActive { get; set; }    // Optional: to filter only active agents
    }
}
