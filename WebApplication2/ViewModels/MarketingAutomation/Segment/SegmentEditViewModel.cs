namespace CRM.WebApp.ViewModels.MarketingAutomation.Segment
{
    public class SegmentEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Criteria { get; set; }
        public bool IsActive { get; set; }
        public string SegmentType { get; set; }
    }
}
