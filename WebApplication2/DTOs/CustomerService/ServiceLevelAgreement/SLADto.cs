namespace CRM.WebApp.DTOs.CustomerService
{
    public class SLADto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ServiceType { get; set; }
        public int ResponseTime { get; set; } // in hours
        public int ResolutionTime { get; set; } // in hours
        public bool IsActive { get; set; }
    }
}
