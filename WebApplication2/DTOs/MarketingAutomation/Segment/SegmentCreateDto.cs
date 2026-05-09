namespace CRM.WebApp.DTOs.MarketingAutomation.Segment
{
    public class SegmentCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Criteria { get; set; }
        public bool IsActive { get; set; }
        public string SegmentType { get; set; }
    }
}
